using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json.Linq;
using Web.Swagger;

namespace Web.Protocols
{
    public class RequestHeader
    {
        [JsonConverter(typeof(StringEnumConverter))]
        public virtual Id ProtocolId { get; set; }

        [SwaggerExcludeAttribute]
        [JsonExtensionData]
        public JObject ExtensionData { get; set; }
    }
}
