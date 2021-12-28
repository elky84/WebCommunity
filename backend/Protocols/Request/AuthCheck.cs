
using EzAspDotNet.Protocols;
using EzAspDotNet.Protocols.Id;

namespace Protocols.Request
{
    public class AuthCheck : RequestHeader
    {
        public override ProtocolId ProtocolId { get { return Id.ProtocolId.AuthCheck; } }

        public string Token { get; set; }
    }
}
