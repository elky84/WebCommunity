using Auth.Models;
using MongoDB.Driver;
using System.Threading.Tasks;
using Protocols.Exception;
using WebUtil.Service;
using WebUtil.Util;
using Protocols.Types;

namespace Auth.Services
{
    public class AccountService
    {
        private MongoDbUtil<AccountData> _mongoDbAccountData;

        private TokenService _tokenService;

        public AccountService(MongoDbService mongoDbServbice, TokenService tokenService)
        {
            _tokenService = tokenService;
            _mongoDbAccountData = new MongoDbUtil<AccountData>(mongoDbServbice.Database);
        }


        public async Task<AccountData> Get(string userId)
        {
            return await _mongoDbAccountData.FindOneAsync(Builders<AccountData>.Filter.Eq(x => x.UserId, userId));
        }

        public async Task<AccountData> SignUp(Protocols.Request.SignUp signUp)
        {
            var accountData = await _mongoDbAccountData.FindOneAsync(Builders<AccountData>.Filter.Eq(x => x.UserId, signUp.UserId));
            if (accountData != null)
            {
                throw new DeveloperException(Protocols.Code.ResultCode.AlreadyTakenUserId, System.Net.HttpStatusCode.Unauthorized);
            }

            accountData = new AccountData { UserId = signUp.UserId, Grade = AccountGradeType.User, State = AccountStateType.Enable, Password = signUp.Password, NickName = signUp.NickName, Email = signUp.Email };

            _tokenService.Issue(accountData);
            await _mongoDbAccountData.CreateAsync(accountData);
            return accountData;
        }

        public async Task<AccountData> Update(string userId, Protocols.Request.AccountUpdate accountUpdate)
        {
            var accountData = await _mongoDbAccountData.FindOneAsync(Builders<AccountData>.Filter.Eq(x => x.UserId, userId));
            if (accountData == null)
            {
                throw new DeveloperException(Protocols.Code.ResultCode.AlreadyTakenUserId);
            }

            if (accountData.State != AccountStateType.Enable)
            {
                throw new DeveloperException(Protocols.Code.ResultCode.NotUsableAccount);
            }

            var update = Builders<AccountData>.Update;
            var definition = update.Set(x => x.NickName, accountUpdate.NickName);
            if (!string.IsNullOrEmpty(accountUpdate.Password))
            {
                definition = definition.Set(x => x.Password, accountUpdate.Password);
            }

            if (!string.IsNullOrEmpty(accountUpdate.Email))
            {
                definition = definition.Set(x => x.Email, accountUpdate.Email);
            }

            await _mongoDbAccountData.UpdateAsync(accountData.Id, definition);
            return accountData;
        }

        public async Task<AccountData> SignIn(Protocols.Request.SignIn signIn)
        {
            var accountData = await _mongoDbAccountData.FindOneAsync(Builders<AccountData>.Filter.Eq(x => x.UserId, signIn.UserId));
            if (accountData == null)
            {
                throw new DeveloperException(Protocols.Code.ResultCode.NotFoundAccount, System.Net.HttpStatusCode.Unauthorized);
            }

            if (accountData.Password != signIn.Password)
            {
                throw new DeveloperException(Protocols.Code.ResultCode.NotMatchPassword, System.Net.HttpStatusCode.Unauthorized);
            }

            _tokenService.Issue(accountData);
            await _mongoDbAccountData.UpdateAsync(accountData.Id, accountData);

            return accountData;
        }

        public async Task<AccountData> SignOut(string userId)
        {
            var accountData = await _mongoDbAccountData.FindOneAsync(Builders<AccountData>.Filter.Eq(x => x.UserId, userId));
            if (accountData == null)
            {
                throw new DeveloperException(Protocols.Code.ResultCode.NotFoundAccount, System.Net.HttpStatusCode.Unauthorized);
            }

            accountData.Token = string.Empty;
            await _mongoDbAccountData.UpdateAsync(accountData.Id, accountData);
            return accountData;
        }
    }
}
