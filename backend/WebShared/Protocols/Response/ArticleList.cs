using System.Collections.Generic;
using Web.Protocols.Page;

namespace Web.Protocols.Response
{
    public class ArticleList : PageResponse<Common.ArticleList>
    {
        public override ProtocolId ProtocolId { get { return ProtocolId.ArticleList; } }
    }
}
