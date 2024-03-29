﻿using Auth.Extend;
using Auth.Services;
using EzAspDotNet.Models;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Auth.Controllers
{
    [Route("Auth/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly AccountService _accountService;

        public AccountController(AccountService accountService)
        {
            _accountService = accountService;
        }

        [HttpPost("Profile")]
        public async Task<Protocols.Response.Account> Profile([FromHeader(Name = EzAspDotNet.Constants.HeaderKeys.AuthorizedUserId)] string userId)
        {
            return (await _accountService.Get(userId)).ToResponse();
        }

        [HttpPost("SignUp")]
        public async Task<Protocols.Response.SignUp> SignUp([FromBody] Protocols.Request.SignUp signUp)
        {
            var account = await _accountService.SignUp(signUp);
            if (account != null)
            {
                Response.TokenSaveToCookie(account.Token);
            }

            return new Protocols.Response.SignUp { Account = MapperUtil.Map<Protocols.Common.Account>(account) };
        }

        [HttpPut("Update")]
        public async Task<Protocols.Response.AccountUpdate> Update([FromHeader(Name = EzAspDotNet.Constants.HeaderKeys.AuthorizedUserId)] string userId,
            [FromBody] Protocols.Request.AccountUpdate update)
        {
            var account = await _accountService.Update(userId, update);
            if (account != null)
            {
                Response.TokenSaveToCookie(account.Token);
            }

            return new Protocols.Response.AccountUpdate { Account = MapperUtil.Map<Protocols.Common.Account>(account) };
        }

        [HttpPost("SignIn")]
        public async Task<Protocols.Response.SignIn> SignIn([FromBody] Protocols.Request.SignIn signIn)
        {
            var account = await _accountService.SignIn(signIn);
            if (account != null)
            {
                Response.TokenSaveToCookie(account.Token);
            }
            return new Protocols.Response.SignIn { Account = MapperUtil.Map<Protocols.Common.Account>(account) };
        }

        [HttpPost("SignOut")]
        public async Task<EzAspDotNet.Protocols.ResponseHeader> SignOut([FromHeader(Name = EzAspDotNet.Constants.HeaderKeys.AuthorizedUserId)] string userId)
        {
            var account = await _accountService.SignOut(userId);
            if (account != null)
            {
                Response.TokenSaveToCookie(account.Token);
            }

            return new EzAspDotNet.Protocols.ResponseHeader { ProtocolId = Protocols.Id.ProtocolId.SignOut };
        }
    }
}