﻿using System;
using System.Threading.Tasks;
using Auth.Extend;
using Auth.Services;
using Microsoft.AspNetCore.Mvc;
using Protocols.Exception;

namespace Auth.Controllers
{
    [Route("Auth/[controller]")]
    [ApiController]
    public class TokenController : ControllerBase
    {
        private readonly TokenService _tokenService;

        public TokenController(TokenService tokenService)
        {
            _tokenService = tokenService;
        }

        [HttpPost("Issue")]
        public async Task<Protocols.Response.Account> Issue([FromBody] Protocols.Request.Authenticate authenticate)
        {
            var data = await _tokenService.Issue(authenticate.UserId);
            if (data != null)
            {
                Response.TokenSaveToCookie(data.Token);
            }
            return data;
        }

        [HttpPost("Validate")]
        public async Task<Protocols.Response.Account> Validate()
        {
            Request.Cookies.TryGetValue("Token", out string token);
            if (string.IsNullOrEmpty(token))
            {
                throw new DeveloperException(Protocols.Code.ResultCode.NotPrivodeToken, System.Net.HttpStatusCode.Unauthorized);
            }

            var data = await _tokenService.Validate(token);
            if (data != null && data.Token != token)
            {
                Response.TokenSaveToCookie(data.Token);
            }
            return data.ToResponse();
        }

        [HttpPost("Refresh")]
        public async Task<Protocols.Response.Account> Refresh([FromBody] Protocols.Request.Authenticate authenticate)
        {
            Request.Cookies.TryGetValue("Token", out string token);

            var data = await _tokenService.Refresh(authenticate.UserId, token);
            if (data != null && data.Token != token)
            {
                Response.TokenSaveToCookie(data.Token);
            }
            return data.ToResponse();
        }

    }
}