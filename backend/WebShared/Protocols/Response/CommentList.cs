using System.Collections.Generic;

namespace Web.Protocols.Response
{
    public class CommentList : ResponseHeader
    {
        public override Id ProtocolId { get { return Id.CommentList; } }

        public List<Common.Comment> Datas { get; set; }
    }
}
