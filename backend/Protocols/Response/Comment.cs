using EzAspDotNet.Protocols;
using EzAspDotNet.Protocols.Id;

namespace Protocols.Response
{
    public class Comment : ResponseHeader
    {
        public override ProtocolId ProtocolId { get { return Id.ProtocolId.Comment; } }

        public Common.Comment Data { get; set; }
    }
}
