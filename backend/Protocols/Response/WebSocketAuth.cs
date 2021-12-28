using EzAspDotNet.Protocols;
using EzAspDotNet.Protocols.Id;

namespace Protocols.Response
{
    public class WebSocketAuth : ResponseHeader
    {
        public override ProtocolId ProtocolId { get { return Id.ProtocolId.WebSocketAuth; } }
    }
}
