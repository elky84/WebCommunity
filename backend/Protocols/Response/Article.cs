using EzAspDotNet.Protocols;
using EzAspDotNet.Protocols.Id;

namespace Protocols.Response
{
    public class Article : ResponseHeader
    {
        public override ProtocolId ProtocolId { get { return Id.ProtocolId.Article; } }

        public Common.Article Data { get; set; }
    }
}
