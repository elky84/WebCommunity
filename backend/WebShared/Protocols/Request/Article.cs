using System;
using System.Collections.Generic;
using System.Text;


namespace Web.Protocols.Request
{
    public class Article : RequestHeader
    {
        public override Id ProtocolId { get { return Id.Article; } }

        public string Title { get; set; }

        public string Content { get; set; }

        public string Author { get; set; }

        public string Category { get; set; }

        public List<string> Tags { get; set; }

        public int Recommend { get; set; }

        public int NotRecommend { get; set; }
    }
}
