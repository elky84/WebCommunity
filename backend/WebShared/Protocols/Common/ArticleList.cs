using System.Collections.Generic;
using Web.Models;

namespace Web.Protocols.Common
{
    public class ArticleList : CommonHeader
    {
        public string Title { get; set; }

        public string Author { get; set; }

        public string Category { get; set; }

        public List<string> Tags { get; set; }

        public int Recommend { get; set; }

        public int NotRecommend { get; set; }
    }
}
