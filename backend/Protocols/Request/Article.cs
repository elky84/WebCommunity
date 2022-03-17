using EzAspDotNet.Protocols;
using EzAspDotNet.Protocols.Id;
using System.Collections.Generic;

namespace Protocols.Request
{
    public class Article : RequestHeader
    {
        public override ProtocolId ProtocolId { get { return Protocols.Id.ProtocolId.Article; } }

        public string Id { get; set; }

        public string Title { get; set; }

        public string Content { get; set; }

        public string Category { get; set; }

        public List<string> Tags { get; set; }

        public string Source { get; set; }

        public int Recommend { get; set; }

        public int NotRecommend { get; set; }
    }
}
