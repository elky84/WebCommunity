using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using EzAspDotNet.Protocols.Common;
using Protocols.Types;

namespace Protocols.Common
{
    public class Account : CommonHeader
    {
        public string UserId { get; set; }

        public string Token { get; set; }

        [JsonConverter(typeof(StringEnumConverter))]
        public AccountGradeType Grade { get; set; }

        [JsonConverter(typeof(StringEnumConverter))]
        public AccountStateType State { get; set; }

        public string Email { get; set; }

        public string NickName { get; set; }
    }
}
