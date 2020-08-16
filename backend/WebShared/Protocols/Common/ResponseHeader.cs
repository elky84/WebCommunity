using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json.Linq;
using Web.Code;
using Web.Swagger;

namespace Web.Protocols
{
    public class ResponseHeader
    {
        [JsonConverter(typeof(StringEnumConverter))]
        public ResultCode ResultCode { get; set; } = ResultCode.Success;

        public string ErrorMessage { get; set; }

        [JsonConverter(typeof(StringEnumConverter))]
        public virtual Id ProtocolId { get; set; }

        [SwaggerExcludeAttribute]
        [JsonExtensionData]
        public JObject ExtensionData { get; set; }
    }
}
