using EzAspDotNet.Protocols;
using EzAspDotNet.Protocols.Id;

namespace Protocols.Request
{
    public class WebSocketAuth : RequestHeader
    {
        public override ProtocolId ProtocolId { get { return Id.ProtocolId.WebSocketAuth; } }

        public string Cookie { get; set; }
    }
}
