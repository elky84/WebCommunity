using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using EzAspDotNet.Protocols;
using EzAspDotNet.Protocols.Id;
using Protocols.Types;

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
        public AccountGradeType Grade { get; set; }

        [JsonConverter(typeof(StringEnumConverter))]
        public AccountStateType State { get; set; }
    }
}
