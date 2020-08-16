using System.ComponentModel.DataAnnotations;
using Web.Protocols.Response;
using Web.Protocols;
using Web.Types;
using System;
using Web.Protocols.Common;
using Web.Models;

namespace Auth.Models
{
    public class AccountData : MongoDbHeader
    {
        public string Token { get; set; }

        public string UserId { get; set; }

        public AccountGradeType Grade { get; set; }

        public AccountStateType State { get; set; }

        public Authenticate ToAuthenticateResponse()
        {
            return new Authenticate
            {
                AccountId = this.Id,
                Token = this.Token,
                UserId = this.UserId,
                Grade = this.Grade
            };
        }

        public Account ToResponse()
        {
            return new Account
            {
                Id = this.Id,
                Token = this.Token,
                UserId = this.UserId,
                Grade = this.Grade,
                State = this.State
            };
        }
    }
}
