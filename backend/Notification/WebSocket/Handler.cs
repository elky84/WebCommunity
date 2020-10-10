using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using NLog;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.WebSockets;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using Web.Protocols;
using WebShared.Util;
using WebUtil.HttpClient;
using WebUtil.RabbitMQ;
using WebUtil.Settings;

namespace Notification.WebSocket
{
    public class Handler : WebUtil.WebSocketManager.Handler
    {
        private class UserData
        {
            public string SocketId { get; set; }

            public IModel UserChannel { get; set; }

            public string UserId { get; set; }

            public IModel ChatChannel { get; set; }

            public string ChannelId { get; set; }
        }

        private readonly Logger logger = LogManager.GetCurrentClassLogger();

        private IConnection _connection;

        private readonly ConcurrentDictionary<string, UserData> UserDatas = new ConcurrentDictionary<string, UserData>();

        private readonly IConfiguration _configuration;

        private readonly IHttpClientFactory _clientFactory;

        public Handler(WebUtil.WebSocketManager.ConnectionManager connectionManager, IHttpClientFactory httpClientFactory, IConfiguration configuration) : base(connectionManager)
        {
            _clientFactory = httpClientFactory;
            _configuration = configuration;
        }

        public bool IsOpen()
        {
            return _connection != null && _connection.IsOpen;
        }

        public void OnUpdate()
        {
            if (IsOpen())
            {
                return;
            }

            try
            {
                var rabbitMqSettings = _configuration.GetRabbitMqSettings();
                _connection = WebUtil.RabbitMQ.Extend.CreateConnection(rabbitMqSettings.Url, rabbitMqSettings.UserName, rabbitMqSettings.Password);

                if (IsOpen())
                {
                    Consumer(null, "Notification", OnReceiveByServer, null);
                }
            }
            catch (Exception e)
            {
                logger.Error($"Catched Exception. [Message:{e.Message}] StackTrace:{e.StackTrace}");
            }
        }


        public override async Task OnConnected(System.Net.WebSockets.WebSocket socket)
        {
            await base.OnConnected(socket);
        }

        public override async Task OnDisconnected(System.Net.WebSockets.WebSocket socket)
        {
            var socketId = ConnectionManager.GetId(socket);
            if (UserDatas.TryRemove(socketId, out var userData))
            {
                userData.ChatChannel?.Close();
                userData.UserChannel?.Close();
            }

            await base.OnDisconnected(socket);
        }

        private string CookieValue(string header, string name)
        {
            Match M = Regex.Match(header, string.Format("{0}=(?<value>.*?);", name));
            return M.ToString().Split('=')[1].Replace(";", "");
        }

        public override async Task ReceiveAsync(System.Net.WebSockets.WebSocket socket, WebSocketReceiveResult result, byte[] buffer)
        {
            if (IsOpen() == false)
            {
                await socket.CloseAsync(WebSocketCloseStatus.EndpointUnavailable, "RabbitMQ is Not Opened.", CancellationToken.None);
                return;
            }


            var requestHeader = JsonConvert.DeserializeObject<RequestHeader>(Encoding.UTF8.GetString(buffer, 0, result.Count));
            await Dispatch(socket, requestHeader);
        }

        private bool IsAuth(System.Net.WebSockets.WebSocket socket)
        {
            var socketId = ConnectionManager.GetId(socket);
            return UserDatas.TryGetValue(socketId, out var userData);
        }


        private async Task Dispatch(System.Net.WebSockets.WebSocket socket, RequestHeader requestHeader)
        {
            var socketId = ConnectionManager.GetId(socket);
            switch (requestHeader.ProtocolId)
            {
                case ProtocolId.WebSocketAuth:
                    {
                        if (IsAuth(socket))
                        {
                            await SendMessageAsync(socket, new Web.Protocols.Response.WebSocketAuth { ResultCode = Web.Code.ResultCode.AlreadyAuth, ErrorMessage = Web.Code.ResultCode.AlreadyAuth.ToString() });
                            return;
                        }

                        var webSocketAuth = requestHeader.ExtensionData.Populate<Web.Protocols.Request.WebSocketAuth>();

                        logger.Debug($"[ReceiveAsync] SocketId:{socketId}, Cookie:{webSocketAuth.Cookie}");

                        var token = CookieValue(webSocketAuth.Cookie, "Token");
                        if (string.IsNullOrEmpty(token))
                        {
                            await socket.CloseAsync(WebSocketCloseStatus.PolicyViolation, "Not found Token", CancellationToken.None);
                            return;
                        }

                        var authCheck = await _clientFactory.RequestJson<Web.Protocols.Response.AuthCheck>(HttpMethod.Post,
                            _configuration.GetGatewaySettings().Url + "/auth/admin/accounts/check",
                            new Web.Protocols.Request.AuthCheck { Token = token });

                        if (authCheck.ResultCode != Web.Code.ResultCode.Success)
                        {
                            await SendMessageAsync(socket, new Web.Protocols.Response.WebSocketAuth { ResultCode = authCheck.ResultCode, ErrorMessage = authCheck.ErrorMessage });
                            await socket.CloseAsync(WebSocketCloseStatus.PolicyViolation, "Invalid Token", CancellationToken.None);
                            return;
                        }

                        var userData = new UserData { UserId = authCheck.UserId, SocketId = socketId };
                        Consumer(socket, authCheck.UserId, OnReceiveByUser, userData);

                        await SendMessageAsync(socket, new Web.Protocols.Response.WebSocketAuth { });
                    }
                    break;
                default:
                    logger.Error($"OnReceiveByUser() Not Implemented Handler.  [Id:{requestHeader.ProtocolId}] [{socketId}]");
                    break;
            }
        }

        private UserData GetUserData(string userId)
        {
            return UserDatas.Values.FirstOrDefault(x => x.UserId == userId);
        }

        private List<UserData> GetUserDatasByChat(string channelId)
        {
            return UserDatas.Values.Where(x => x.ChannelId == channelId).ToList();
        }

        private async void OnReceiveByUser(string queueName, object model, BasicDeliverEventArgs ea)
        {
            var userData = GetUserData(queueName);
            if (userData == null)
            {
                logger.Debug($"OnReceiveByUser() Not found User [{queueName}]");
                return;
            }

            var socket = ConnectionManager.GetSocketById(userData.SocketId);
            var str = Encoding.UTF8.GetString(ea.Body);
            logger.Debug($"OnReceiveByUser() Consumer [{userData.SocketId}] [{queueName}] {str}");
            await SendMessageAsync(socket, str);
        }

        private async void OnReceiveByChat(string queueName, object model, BasicDeliverEventArgs ea)
        {
            foreach (var userData in GetUserDatasByChat(queueName))
            {
                var socket = ConnectionManager.GetSocketById(userData.SocketId);
                var str = Encoding.UTF8.GetString(ea.Body);
                logger.Debug($"OnReceiveByChat() Consumer [{userData.SocketId}] [{queueName}] {str}");
                await SendMessageAsync(socket, str);
            }
        }

        private async void OnReceiveByServer(string queueName, object model, BasicDeliverEventArgs ea)
        {
            var str = Encoding.UTF8.GetString(ea.Body);
            logger.Debug($"OnReceiveByServer() Consumer [{queueName}] {str}");
            var requestHeader = JsonConvert.DeserializeObject<RequestHeader>(str);
            switch (requestHeader.ProtocolId)
            {
                case ProtocolId.AdminDisconnectUser:
                    {
                        var disconnectUser = requestHeader.ExtensionData.Populate<Web.Protocols.Admin.Request.DisconnectUser>();
                        var userData = GetUserData(disconnectUser.UserId);
                        if (null == userData)
                        {
                            logger.Error($"OnReceiveByUser() Not Found User Socket by UserId. Maybe already disconnected user.  [Id:{requestHeader.ProtocolId}] [{queueName}] {str}");
                            break;
                        }

                        var userSocket = ConnectionManager.GetSocketById(userData.SocketId);
                        if (userSocket == null)
                        {
                            logger.Error($"OnReceiveByUser() Not Found User Socket. Maybe already disconnected user. [Id:{requestHeader.ProtocolId}] [{queueName}] {str}");
                            break;
                        }

                        await SendMessageAsync(userSocket, new Web.Protocols.Response.WebSocketAuth { ResultCode = Web.Code.ResultCode.DisconnectByServer });
                        await CloseSocket(userSocket, "Disconnect User by Server");
                        break;
                    }
                default:
                    {
                        logger.Error($"OnReceiveByUser() Not Implemented Handler.  [Id:{requestHeader.ProtocolId}] [{queueName}] {str}");
                        break;
                    }
            }
        }

        private async Task CloseSocket(System.Net.WebSockets.WebSocket userSocket, string message)
        {
            try
            {
                await userSocket.CloseAsync(WebSocketCloseStatus.PolicyViolation, message, CancellationToken.None);
            }
            catch (WebSocketException e)
            {
                logger.Error($"CloseSocket() catched Exception: {e.Message}");
            }
        }

        private delegate void OnReceiveDelegate(string queueName, object model, BasicDeliverEventArgs ea);

        private IModel Consumer(System.Net.WebSockets.WebSocket socket, string queueName, OnReceiveDelegate onReceiveDelegate,
            UserData newUserData)
        {
            if (newUserData != null)
            {
                var userData = GetUserData(queueName);
                if (userData != null && userData.UserChannel != null)
                {
                    userData.UserChannel.Close();
                    UserDatas.TryRemove(userData.SocketId, out var beforeUserData);
                }
            }

            var channel = _connection.CreateChannel();
            var consumer = new EventingBasicConsumer(channel);
            consumer.Received += (model, ea) => onReceiveDelegate(queueName, model, ea);

            channel.Consume(queueName, consumer);

            if (newUserData != null)
            {
                newUserData.UserId = queueName;
                newUserData.UserChannel = channel;
                UserDatas.TryAdd(newUserData.SocketId, newUserData);
            }

            return channel;
        }

        private bool ConsumerByChat(System.Net.WebSockets.WebSocket socket, string queueName, OnReceiveDelegate onReceiveDelegate, UserData userData)
        {
            userData.ChatChannel?.Close();

            var channel = _connection.CreateChannel();
            var consumer = new EventingBasicConsumer(channel);
            consumer.Received += (model, ea) => onReceiveDelegate(queueName, model, ea);

            var consumerCount = channel.ConsumerCount(queueName);
            //TODO 상수 치환 필요
            if (consumerCount >= 25)
            {
                return false;
            }

            channel.Consume(queueName, consumer);

            userData.ChannelId = queueName;
            userData.ChatChannel = channel;
            UserDatas.TryAdd(userData.SocketId, userData);

            return true;
        }
    }
}
