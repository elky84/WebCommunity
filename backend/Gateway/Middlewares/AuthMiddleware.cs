using Microsoft.Extensions.Primitives;
using Newtonsoft.Json;
using Ocelot.Middleware;
using System;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Runtime.Caching;
using System.Threading.Tasks;
using Web.Protocols.Exception;

namespace Gateway.Middlewares
{
    public class AuthMiddleware
    {
        private readonly IHttpClientFactory _clientFactory;

        private Random Random = new Random();

        public AuthMiddleware(IHttpClientFactory clientFactory)
        {
            _clientFactory = clientFactory;
        }

        public async Task Authenticate(DownstreamContext ctx, Func<Task> next)
        {
            if (ctx.DownstreamRequest.Headers.TryGetValues(WebUtil.Constants.HeaderKeys.InternalServer, out var server))
            {
                await next.Invoke();
                return;
            }

#if DEBUG
            await next.Invoke();
            return;
#else
            var uri = ctx.DownstreamRequest.AbsolutePath;
            if (uri.StartsWith("/Auth/Token/Issue")) // TODO white 리스트를 어떻게 관리 할 것인가?
            {
                await next.Invoke();
                return;
            }

            string userId = await Authorization(ctx);
            ctx.DownstreamRequest.Headers.Add(WebUtil.Constants.HeaderKeys.AuthorizedUserId, userId);

            await next.Invoke();
#endif
        }

        private async Task<string> Authorization(DownstreamContext ctx)
        {
            ctx.HttpContext.Request.Headers.TryGetValue(Web.Constants.HeaderKeys.Cookie, out StringValues values);
            var cacheKey = values.ToString();

            ObjectCache cache = MemoryCache.Default;
            var userId = cache[cacheKey] as string;
            if (userId != null)
            {
                return userId;
            }

            var reRoute = Configuration.Extend.Get("AuthReRoute", "/Auth/{everything}");
            if (reRoute == null)
            {
                throw new LogicException(Web.Code.ResultCode.NotFoundAuthRoutes);
            }

            var downStreamHostAndPorts = reRoute.DownstreamHostAndPorts[Random.Next(reRoute.DownstreamHostAndPorts.Count)];
            var request = new HttpRequestMessage(HttpMethod.Post, $"{reRoute.DownstreamScheme}://{downStreamHostAndPorts.Host}:{downStreamHostAndPorts.Port}/Auth/Token/Validate");

            var client = _clientFactory.CreateClient();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            request.Headers.Add(Web.Constants.HeaderKeys.Cookie, values.AsEnumerable());

            var response = await client.SendAsync(request);
            var content = await response.Content.ReadAsStringAsync();
            if (response.StatusCode != System.Net.HttpStatusCode.OK)
            {
                var error = JsonConvert.DeserializeObject<ErrorDetails>(content);
                throw new LogicException(error.ResultCode, response.StatusCode, error.Detail);
            }
            else
            {
                var auth = JsonConvert.DeserializeObject<Web.Protocols.Response.Authenticate>(content);
                if (auth.ResultCode != Web.Code.ResultCode.Success)
                {
                    throw new LogicException(auth.ResultCode, response.StatusCode, auth.ErrorMessage);
                }

                if (response.Headers.TryGetValues(Web.Constants.HeaderKeys.SetCookie, out var setCookie))
                {
                    ctx.HttpContext.Request.Headers.Remove(Web.Constants.HeaderKeys.Cookie);
                    ctx.HttpContext.Request.Headers.Add(Web.Constants.HeaderKeys.Cookie, setCookie.ToArray());
                    ctx.HttpContext.Response.Headers.Add(Web.Constants.HeaderKeys.SetCookie, setCookie.ToArray());
                }

                var policy = new CacheItemPolicy();
                cache.Set(cacheKey, auth.UserId, policy);
                return auth.UserId;
            }
        }
    }
}
