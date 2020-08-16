using System.Collections.Generic;

namespace Web.Protocols.Response
{
    public class ArticleList : ResponseHeader
    {
        public override Id ProtocolId { get { return Id.ArticleList; } }

        public List<Common.ArticleList> Datas { get; set; }
    }
}
