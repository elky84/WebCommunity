using Microsoft.Extensions.Primitives;
using Newtonsoft.Json;
using Ocelot.Middleware;
using System;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Runtime.Caching;
using System.Threading.Tasks;
using System.Web;
using Protocols.Exception;
using Ocelot.Configuration.File;
using System.Collections.Generic;
using System.IO;

namespace Gateway.Middlewares
{
    public class AuthMiddleware
    {
        private readonly IHttpClientFactory _clientFactory;

        private readonly Random Random = new();

        public AuthMiddleware(IHttpClientFactory clientFactory)
        {
            _clientFactory = clientFactory;
        }

        public async Task Authenticate(DownstreamContext ctx, Func<Task> next)
        {
            if (ctx.DownstreamRequest.Headers.TryGetValues(EzAspDotNet.Constants.HeaderKeys.InternalServer, out var server))
            {
                await next.Invoke();
                return;
            }

            var uri = ctx.DownstreamRequest.AbsolutePath;
            if (uri.StartsWith("/Auth/Account/SignUp") ||
                uri.StartsWith("/Auth/Account/SignIn") ||
                (uri.StartsWith("/Board") && uri.EndsWith("/Read")) ||
                ctx.DownstreamRequest.Method == HttpMethod.Get.Method) // TODO white 리스트를 어떻게 관리 할 것인가?
            {
                await next.Invoke();
                return;
            }

            var auth = await Authorization(ctx);
            ctx.DownstreamRequest.Headers.Add(EzAspDotNet.Constants.HeaderKeys.AuthorizedUserId, auth.UserId);
            ctx.DownstreamRequest.Headers.Add(WebUtil.HeaderKeys.AuthorizedNickName, HttpUtility.UrlEncode(auth.NickName));
            await next.Invoke();
        }

        private async Task<Protocols.Response.Account> Authorization(DownstreamContext ctx)
        {
            ctx.HttpContext.Request.Headers.TryGetValue(EzAspDotNet.Constants.HeaderKeys.Cookie, out StringValues values);
            var cacheKey = values.ToString();

            ObjectCache cache = MemoryCache.Default;
            if (cache[cacheKey] is Protocols.Response.Account cacheAuth)
            {
                return cacheAuth;
            }

            var reRoute = GetReRoute("AuthReRoute", "/Auth/{everything}");
            if (reRoute == null)
            {
                throw new DeveloperException(Protocols.Code.ResultCode.NotFoundAuthRoutes);
            }

            var downStreamHostAndPorts = reRoute.DownstreamHostAndPorts[Random.Next(reRoute.DownstreamHostAndPorts.Count)];
            var request = new HttpRequestMessage(HttpMethod.Post, $"{reRoute.DownstreamScheme}://{downStreamHostAndPorts.Host}:{downStreamHostAndPorts.Port}/Auth/Token/Validate");

            var client = _clientFactory.CreateClient();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            request.Headers.Add(EzAspDotNet.Constants.HeaderKeys.Cookie, values.AsEnumerable());

            var response = await client.SendAsync(request);
            var content = await response.Content.ReadAsStringAsync();
            if (response.StatusCode != System.Net.HttpStatusCode.OK)
            {
                var error = JsonConvert.DeserializeObject<ErrorDetails>(content);
                throw new DeveloperException(error.ResultCode, response.StatusCode, error.Detail);
            }
            else
            {
                var auth = JsonConvert.DeserializeObject<Protocols.Response.Account>(content);
                if (auth.ResultCode != EzAspDotNet.Code.ResultCode.Success)
                {
                    throw new DeveloperException(auth.ResultCode, response.StatusCode);
                }

                if (response.Headers.TryGetValues(EzAspDotNet.Constants.HeaderKeys.SetCookie, out var setCookie))
                {
                    ctx.HttpContext.Request.Headers.Remove(EzAspDotNet.Constants.HeaderKeys.Cookie);
                    ctx.HttpContext.Request.Headers.Add(EzAspDotNet.Constants.HeaderKeys.Cookie, setCookie.ToArray());
                    ctx.HttpContext.Response.Headers.Add(EzAspDotNet.Constants.HeaderKeys.SetCookie, setCookie.ToArray());
                }

                var policy = new CacheItemPolicy();
                cache.Set(cacheKey, auth.UserId, policy);
                return auth;
            }
        }

        public static FileReRoute GetReRoute(string cacheKey, string pathTemplate)
        {
            ObjectCache cache = MemoryCache.Default;
            var reRoute = cache[cacheKey] as FileReRoute;
            if (reRoute == null)
            {
                var fileName = $"./ocelot.{Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT")}.json";
                var filePaths = new List<string> { fileName };

                var policy = new CacheItemPolicy();
                policy.ChangeMonitors.Add(new HostFileChangeMonitor(filePaths));

                reRoute = JsonConvert.DeserializeObject<FileConfiguration>(File.ReadAllText(fileName)).ReRoutes
                    .FirstOrDefault(x => x.UpstreamPathTemplate.StartsWith(pathTemplate) && x.DownstreamHostAndPorts.Count() > 0);

                if (reRoute != null)
                {
                    cache.Set(cacheKey, reRoute, policy);
                }
            }
            return reRoute;
        }
    }
}
