using System.ComponentModel.DataAnnotations;
using Web.Protocols.Response;
using Web.Protocols;
using Web.Types;
using System;
using Web.Protocols.Common;
using Web.Models;
using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;

namespace Auth.Models
{
    public class AccountData : MongoDbHeader
    {
        public string Token { get; set; }

        public string UserId { get; set; }

        [BsonRepresentation(BsonType.String)]
        public AccountGradeType Grade { get; set; }

        [BsonRepresentation(BsonType.String)]
        public AccountStateType State { get; set; }

        public string Password { get; set; }

        public string Email { get; set; }

        public string NickName { get; set; }

        public Web.Protocols.Response.Account ToResponse()
        {
            return new Web.Protocols.Response.Account
            {
                AccountId = this.Id,
                Token = this.Token,
                UserId = this.UserId,
                Grade = this.Grade,
                NickName = this.NickName
            };
        }

        public Web.Protocols.Common.Account ToCommon()
        {
            return new Web.Protocols.Common.Account
            {
                Id = this.Id,
                Token = this.Token,
                UserId = this.UserId,
                Grade = this.Grade,
                State = this.State,
                Email = this.Email,
                NickName = this.NickName
            };
        }
    }
}
