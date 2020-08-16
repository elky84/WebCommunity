using System;
using System.Threading.Tasks;
using Auth.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NLog;

namespace Auth.Controllers
{
    [Route("Auth/[controller]")]
    [ApiController]
    public class TokenController : ControllerBase
    {
        private static readonly Logger logger = LogManager.GetCurrentClassLogger();

        private readonly TokenService _tokenService;

        public TokenController(TokenService tokenService)
        {
            _tokenService = tokenService;
        }

        [HttpPost("Issue")]
        public async Task<Web.Protocols.Response.Authenticate> Issue([FromBody] Web.Protocols.Request.Authenticate authenticate)
        {
            var data = await _tokenService.Issue(authenticate.UserId);
            if (data != null)
            {
                TokenSaveToCookie(Response, data.Token);
            }
            return data;
        }

        [HttpPost("Validate")]
        public async Task<Web.Protocols.Response.Authenticate> Validate()
        {
            Request.Cookies.TryGetValue("Token", out string token);
            var data = await _tokenService.Validate(token);
            if (data != null && data.Token != token)
            {
                TokenSaveToCookie(Response, data.Token);
            }
            return data.ToAuthenticateResponse();
        }

        [HttpPost("Refresh")]
        public async Task<Web.Protocols.Response.Authenticate> Refresh([FromBody] Web.Protocols.Request.Authenticate authenticate)
        {
            Request.Cookies.TryGetValue("Token", out string token);

            var data = await _tokenService.Refresh(authenticate.UserId, token);
            if (data != null && data.Token != token)
            {
                TokenSaveToCookie(Response, data.Token);
            }
            return data.ToAuthenticateResponse();
        }

        private void TokenSaveToCookie(HttpResponse response, string token)
        {
            response.Cookies.Delete(token);

            var options = new CookieOptions
            {
                //Expires = DateTime.Now.AddMinutes(60),
                IsEssential = true
            };

            response.Cookies.Append("Token", token, options);
        }
    }
}