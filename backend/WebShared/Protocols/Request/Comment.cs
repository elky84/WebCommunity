using System;
using System.Collections.Generic;
using System.Text;


namespace Web.Protocols.Request
{
    public class Comment : RequestHeader
    {
        public override ProtocolId ProtocolId { get { return ProtocolId.Comment; } }

        public string OriginCommentId { get; set; }

        public string Content { get; set; }

        public string Author { get; set; }

        public int Recommend { get; set; }

        public int NotRecommend { get; set; }
    }
}
