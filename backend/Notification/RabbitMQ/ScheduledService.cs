using Notification.WebSocket;
using Serilog;
using System;
using System.Threading;
using System.Threading.Tasks;
using WebUtil.Service;

namespace Notification.RabbitMQ
{
    public class ScheduledService : RepeatedService
    {
        private Handler _handler;

        public ScheduledService(Handler handler)
        {
            _handler = handler;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            // This will cause the loop to stop if the service is stopped
            while (!stoppingToken.IsCancellationRequested)
            {
                try
                {
                    _handler.OnUpdate();
                }
                catch (Exception e)
                {
                    Log.Logger.Error($"update server list from registry failed. Reason:{e.Message}");

                }

                await Task.Delay(TimeSpan.FromSeconds(5), stoppingToken);
            }
        }
    }
}
