using EzAspDotNet.Protocols;
using EzAspDotNet.Protocols.Id;

namespace Protocols.Request
{
    public class AccountUpdate : RequestHeader
    {
        public override ProtocolId ProtocolId { get { return Id.ProtocolId.AccountUpdate; } }

        public string Password { get; set; }

        public string Email { get; set; }

        public string NickName { get; set; }
    }
}
