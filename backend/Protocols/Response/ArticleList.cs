
using EzAspDotNet.Protocols.Id;
using EzAspDotNet.Protocols.Page;

namespace Protocols.Response
{
    public class ArticleList : PageResponse<Common.ArticleList>
    {
        public override ProtocolId ProtocolId { get { return Id.ProtocolId.ArticleList; } }
    }
}
