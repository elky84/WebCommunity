using System.Collections.Generic;
using Web.Models;

namespace Board.Models
{
    public class Comment : MongoDbHeader
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
