using System.Collections.Generic;

namespace Web.Protocols.Response
{
    public class CommentList : ResponseHeader
    {
        public override ProtocolId ProtocolId { get { return ProtocolId.CommentList; } }

        public List<Common.Comment> Datas { get; set; }

        public long Total { get; set; }
    }
}
