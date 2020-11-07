using System.Collections.Generic;
using Web.Models;

namespace Web.Protocols.Common
{
    public class Article : CommonHeader
    {
        public string Title { get; set; }

        public string Content { get; set; }

        public string Author { get; set; }

        public string UserId { get; set; }

        public string Category { get; set; }

        public List<string> Tags { get; set; }

        public string Source { get; set; }

        public int Recommend { get; set; }

        public int NotRecommend { get; set; }

        public int Hit { get; set; }
    }
}
