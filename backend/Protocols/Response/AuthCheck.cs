using EzAspDotNet.Protocols;
using EzAspDotNet.Protocols.Id;

namespace Protocols.Response
{
    public class AuthCheck : ResponseHeader
    {
        public override ProtocolId ProtocolId { get { return Id.ProtocolId.AuthCheck; } }

        public string UserId { get; set; }
    }
}
