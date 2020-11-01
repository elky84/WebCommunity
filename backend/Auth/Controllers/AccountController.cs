using System;
using System.Threading.Tasks;
using Auth.Extend;
using Auth.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NLog;

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

        [HttpPost("SignUp")]
        public async Task<Web.Protocols.Response.SignUp> SignUp([FromBody] Web.Protocols.Request.SignUp signUp)
        {
            var account = await _accountService.SignUp(signUp);
            if (account != null)
            {
                Response.TokenSaveToCookie(account.Token);
            }

            return new Web.Protocols.Response.SignUp { Account = account.ToResponse() };
        }

        [HttpPut("Update")]
        public async Task<Web.Protocols.ResponseHeader> Update([FromHeader(Name = WebUtil.HeaderKeys.AuthorizedUserId)] string userId,
            [FromBody] Web.Protocols.Request.AccountUpdate update)
        {
            var account = await _accountService.Update(userId, update);
            if (account != null)
            {
                Response.TokenSaveToCookie(account.Token);
            }

            return new Web.Protocols.ResponseHeader { ProtocolId = Web.Protocols.ProtocolId.AccountUpdate };
        }

        [HttpPost("SignIn")]
        public async Task<Web.Protocols.ResponseHeader> SignIn([FromBody] Web.Protocols.Request.SignIn signIn)
        {
            var account = await _accountService.SignIn(signIn);
            if (account != null)
            {
                Response.TokenSaveToCookie(account.Token);
            }
            return new Web.Protocols.ResponseHeader { ProtocolId = Web.Protocols.ProtocolId.SignIn };
        }

        [HttpPost("SignOut")]
        public async Task<Web.Protocols.ResponseHeader> SignOut([FromHeader(Name = WebUtil.HeaderKeys.AuthorizedUserId)] string userId)
        {
            var account = await _accountService.SignOut(userId);
            if (account != null)
            {
                Response.TokenSaveToCookie(account.Token);
            }

            return new Web.Protocols.ResponseHeader { ProtocolId = Web.Protocols.ProtocolId.SignOut };
        }
    }
}