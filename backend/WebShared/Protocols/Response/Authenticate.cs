using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;
using System.Collections.Generic;
using System.Text;
using Web.Types;

namespace Web.Protocols.Response
{
    public class Authenticate : ResponseHeader
    {
        public override Id ProtocolId { get { return Id.Authenticate; } }

        public string AccountId { get; set; }

        public string UserId { get; set; }

        [JsonIgnore]
        public string Token { get; set; }


        [JsonConverter(typeof(StringEnumConverter))]
        public AccountGradeType Grade { get; set; }

        [JsonConverter(typeof(StringEnumConverter))]
        public AccountStateType State { get; set; }
    }
}
