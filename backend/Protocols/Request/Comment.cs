using EzAspDotNet.Protocols;
using EzAspDotNet.Protocols.Id;

namespace Protocols.Request
{
    public class Comment : RequestHeader
    {
        public override ProtocolId ProtocolId { get { return Id.ProtocolId.Comment; } }

        public string OriginCommentId { get; set; }

        public string Content { get; set; }

        public int Recommend { get; set; }

        public int NotRecommend { get; set; }
    }
}
