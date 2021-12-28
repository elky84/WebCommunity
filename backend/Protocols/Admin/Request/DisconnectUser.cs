using EzAspDotNet.Protocols;
using EzAspDotNet.Protocols.Id;

namespace Protocols.Admin.Request
{
    public class DisconnectUser : RequestHeader
    {
        public override ProtocolId ProtocolId { get { return Id.ProtocolId.AdminDisconnectUser; } }

        public string UserId { get; set; }
    }
}
