using System.Collections.Generic;
using Web.Models;

namespace Web.Protocols.Common
{
    public class Comment : CommonHeader
    {
        public string ArticleId { get; set; }

        public string CommentId { get; set; }

        public string Content { get; set; }

        public string Author { get; set; }

        public string OriginAuthor { get; set; }

        public int Recommend { get; set; }

        public int NotRecommend { get; set; }
    }
}
