using EzAspDotNet.Protocols;
using EzAspDotNet.Protocols.Id;

namespace Protocols.Response
{
    public class SignIn : ResponseHeader
    {
        public override ProtocolId ProtocolId { get { return Id.ProtocolId.SignIn; } }

        public Common.Account Account { get; set; }
    }
}
