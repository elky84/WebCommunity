using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;
using Protocols.Types;
using MongoDbWebUtil.Models;

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

        public Protocols.Response.Account ToResponse()
        {
            return new Protocols.Response.Account
            {
                AccountId = this.Id,
                Token = this.Token,
                UserId = this.UserId,
                Grade = this.Grade,
                NickName = this.NickName
            };
        }

        public Protocols.Common.Account ToCommon()
        {
            return new Protocols.Common.Account
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
