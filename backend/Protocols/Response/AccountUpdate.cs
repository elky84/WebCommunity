using EzAspDotNet.Protocols;
using EzAspDotNet.Protocols.Id;


namespace Protocols.Response
{
    public class AccountUpdate : ResponseHeader
    {
        public override ProtocolId ProtocolId { get { return Id.ProtocolId.AccountUpdate; } }

        public Common.Account Account { get; set; }
    }
}
