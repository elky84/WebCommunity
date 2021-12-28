using EzAspDotNet.Protocols;
using EzAspDotNet.Protocols.Id;

namespace Protocols.Request
{
    public class Authenticate : RequestHeader
    {
        public override ProtocolId ProtocolId { get { return Id.ProtocolId.Authenticate; } }

        public string UserId { get; set; }
    }
}
