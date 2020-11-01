using Auth.Models;
using Auth.Models.Settings;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Net.Http;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Web.Protocols.Exception;
using Web.Types;
using WebUtil.Service;
using WebUtil.Util;

namespace Auth.Services
{
    public class TokenService
    {
        private readonly AppSettings _appSettings;

        private readonly JwtSecurityTokenHandler _tokenHandler = new JwtSecurityTokenHandler();

        private const string UserIdName = "UserId";

        private MongoDbUtil<AccountData> _mongoDbAccountData;

        public TokenService(MongoDbService mongoDbServbice, IOptions<AppSettings> appSettings)
        {
            _appSettings = appSettings.Value;
            _mongoDbAccountData = new MongoDbUtil<AccountData>(mongoDbServbice.Database);
        }

        public async Task<Web.Protocols.Response.Authenticate> Issue(string userId)
        {
            if (string.IsNullOrEmpty(userId) || userId.ToCharArray().Where(x => char.GetUnicodeCategory(x) == System.Globalization.UnicodeCategory.OtherLetter).Count() > 0)
            {
                throw new DeveloperException(Web.Code.ResultCode.InvalidUserId, System.Net.HttpStatusCode.BadRequest);
            }

            var accountData = await _mongoDbAccountData.FindOneAsync(Builders<AccountData>.Filter.Eq(x => x.UserId, userId));
            if (accountData == null)
            {
                accountData = new AccountData { UserId = userId, Grade = AccountGradeType.User, State = AccountStateType.Enable };
                await _mongoDbAccountData.CreateAsync(accountData);
                throw new DeveloperException(Web.Code.ResultCode.NotFoundAccount, System.Net.HttpStatusCode.Unauthorized);
            }

            if (accountData.State != AccountStateType.Enable)
            {
                throw new DeveloperException(Web.Code.ResultCode.NotUsableAccount, System.Net.HttpStatusCode.Unauthorized);
            }

            Issue(accountData);

            return accountData.ToAuthenticateResponse();
        }

        public void Issue(AccountData accountData)
        {
            // authentication successful so generate jwt token
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[] { new Claim(UserIdName, accountData.UserId) }),
                Expires = DateTime.UtcNow.AddDays(30),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(Encoding.ASCII.GetBytes(_appSettings.Secret)), SecurityAlgorithms.HmacSha256Signature)
            };

            var token = _tokenHandler.CreateToken(tokenDescriptor);
            accountData.Token = _tokenHandler.WriteToken(token);
        }

        public async Task<AccountData> Validate(string token)
        {
            var pair = await Check(token);
            var remain = pair.Key.ValidTo.Subtract(DateTime.UtcNow);
            if (remain.TotalDays <= 7.0)
            {
                Issue(pair.Value);
            }
            return pair.Value;
        }

#pragma warning disable 1998
        public async Task<KeyValuePair<SecurityToken, AccountData>> Check(string token)
        {
            var claimsPrincipal = _tokenHandler.ValidateToken(token, new TokenValidationParameters
            {
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(_appSettings.Secret)),
                ValidateIssuer = false,
                ValidateAudience = false
            }, out var securityToken);

            var dic = claimsPrincipal.Claims.ToDictionary(x => x.Type);

            var accountData = await _mongoDbAccountData.FindOneAsync(Builders<AccountData>.Filter.Eq(x => x.UserId, dic[UserIdName].Value));
            if (accountData == null)
            {
                throw new DeveloperException(Web.Code.ResultCode.NotFoundAccount, System.Net.HttpStatusCode.BadRequest);
            }

            if (string.IsNullOrEmpty(token))
            {
                throw new DeveloperException(Web.Code.ResultCode.NotPrivodedToken, System.Net.HttpStatusCode.Unauthorized);
            }

            if (accountData.State != AccountStateType.Enable)
            {
                throw new DeveloperException(Web.Code.ResultCode.NotUsableAccount, System.Net.HttpStatusCode.Unauthorized);
            }

            if (token != accountData.Token)
            {
                throw new DeveloperException(Web.Code.ResultCode.InvalidToken, System.Net.HttpStatusCode.Unauthorized);
            }
            return new KeyValuePair<SecurityToken, AccountData>(securityToken, accountData);
        }
#pragma warning restore 1998

        private async Task<AccountData> Validate(string userId, string token)
        {
            var accountData = await Validate(token);
            if (userId != accountData.UserId)
            {
                throw new DeveloperException(Web.Code.ResultCode.InvalidToken, System.Net.HttpStatusCode.Unauthorized);
            }
            return accountData;
        }

        public async Task<AccountData> Refresh(string userId, string token)
        {
            var accountData = await Validate(userId, token);
            Issue(accountData);
            return accountData;
        }
    }
}
