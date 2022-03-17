using EzAspDotNet.Protocols;
using EzAspDotNet.Protocols.Id;
using System.Collections.Generic;

namespace Protocols.Response
{
    public class CommentList : ResponseHeader
    {
        public override ProtocolId ProtocolId { get { return Id.ProtocolId.CommentList; } }

        public List<Common.Comment> Datas { get; set; }

        public long Total { get; set; }
    }
}
