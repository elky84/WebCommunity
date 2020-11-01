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
            if (ctx.DownstreamRequest.Headers.TryGetValues(WebUtil.HeaderKeys.InternalServer, out var server))
            {
                await next.Invoke();
                return;
            }

            var uri = ctx.DownstreamRequest.AbsolutePath;
            if (uri.StartsWith("/Auth/Account/SignUp") ||
                uri.StartsWith("/Auth/Account/SignIn") ||
                ctx.DownstreamRequest.Method == HttpMethod.Get.Method) // TODO white 리스트를 어떻게 관리 할 것인가?
            {
                await next.Invoke();
                return;
            }

            var auth = await Authorization(ctx);
            ctx.DownstreamRequest.Headers.Add(WebUtil.HeaderKeys.AuthorizedUserId, auth.UserId);
            ctx.DownstreamRequest.Headers.Add(WebUtil.HeaderKeys.AuthorizedNickName, HttpUtility.UrlEncode(auth.NickName));
            await next.Invoke();
        }

        private async Task<Web.Protocols.Response.Authenticate> Authorization(DownstreamContext ctx)
        {
            ctx.HttpContext.Request.Headers.TryGetValue(Web.Constants.HeaderKeys.Cookie, out StringValues values);
            var cacheKey = values.ToString();

            ObjectCache cache = MemoryCache.Default;
            var cacheAuth = cache[cacheKey] as Web.Protocols.Response.Authenticate;
            if (cacheAuth != null)
            {
                return cacheAuth;
            }

            var reRoute = Configuration.Extend.Get("AuthReRoute", "/Auth/{everything}");
            if (reRoute == null)
            {
                throw new DeveloperException(Web.Code.ResultCode.NotFoundAuthRoutes);
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
                throw new DeveloperException(error.ResultCode, response.StatusCode, error.Detail);
            }
            else
            {
                var auth = JsonConvert.DeserializeObject<Web.Protocols.Response.Authenticate>(content);
                if (auth.ResultCode != Web.Code.ResultCode.Success)
                {
                    throw new DeveloperException(auth.ResultCode, response.StatusCode, auth.ErrorMessage);
                }

                if (response.Headers.TryGetValues(Web.Constants.HeaderKeys.SetCookie, out var setCookie))
                {
                    ctx.HttpContext.Request.Headers.Remove(Web.Constants.HeaderKeys.Cookie);
                    ctx.HttpContext.Request.Headers.Add(Web.Constants.HeaderKeys.Cookie, setCookie.ToArray());
                    ctx.HttpContext.Response.Headers.Add(Web.Constants.HeaderKeys.SetCookie, setCookie.ToArray());
                }

                var policy = new CacheItemPolicy();
                cache.Set(cacheKey, auth.UserId, policy);
                return auth;
            }
        }
    }
}
