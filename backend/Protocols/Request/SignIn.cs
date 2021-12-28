
using EzAspDotNet.Protocols;
using EzAspDotNet.Protocols.Id;

namespace Protocols.Request
{
    public class SignIn : RequestHeader
    {
        public override ProtocolId ProtocolId { get { return Id.ProtocolId.SignIn; } }

        public string UserId { get; set; }

        public string Password { get; set; }
    }
}
