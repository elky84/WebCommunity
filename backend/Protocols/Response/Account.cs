using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using EzAspDotNet.Protocols;
using EzAspDotNet.Protocols.Id;
using Protocols.Code;

namespace Protocols.Response
{
    public class Account : ResponseHeader
    {
        public override ProtocolId ProtocolId { get { return Id.ProtocolId.Authenticate; } }

        public string AccountId { get; set; }

        public string UserId { get; set; }

        public string NickName { get; set; }

        [JsonIgnore]
        public string Token { get; set; }

        [JsonConverter(typeof(StringEnumConverter))]
        public AccountGrade Grade { get; set; }

        [JsonConverter(typeof(StringEnumConverter))]
        public AccountState State { get; set; }
    }
}
