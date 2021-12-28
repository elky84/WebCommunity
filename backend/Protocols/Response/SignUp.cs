using EzAspDotNet.Protocols;
using EzAspDotNet.Protocols.Id;

namespace Protocols.Response
{
    public class SignUp : ResponseHeader
    {
        public override ProtocolId ProtocolId { get { return Id.ProtocolId.SignUp; } }

        public Common.Account Account { get; set; }
    }
}
