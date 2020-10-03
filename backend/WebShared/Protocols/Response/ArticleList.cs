using System.Collections.Generic;
using Web.Protocols.Page;

namespace Web.Protocols.Response
{
    public class ArticleList : PageResponse<Common.ArticleList>
    {
        public override Id ProtocolId { get { return Id.ArticleList; } }
    }
}
