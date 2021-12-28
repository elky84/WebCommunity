
using EzAspDotNet.Protocols;
using EzAspDotNet.Protocols.Id;

namespace Protocols.Request
{
    public class SignUp : RequestHeader
    {
        public override ProtocolId ProtocolId { get { return Id.ProtocolId.SignUp; } }

        public string UserId { get; set; }

        public string Password { get; set; }

        public string Email { get; set; }

        public string NickName { get; set; }
    }
}
