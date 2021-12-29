using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using MongoDbWebUtil.Models;
using Protocols.Code;

namespace Board.Models
{
    public class Comment : MongoDbHeader
    {
        public string ArticleId { get; set; }

        public string CommentId { get; set; }

        public string Content { get; set; }

        public string Author { get; set; }

        public string UserId { get; set; }

        public string OriginAuthor { get; set; }

        public int Recommend { get; set; }

        public int NotRecommend { get; set; }

        [BsonRepresentation(BsonType.String)]
        public CommentStatus Status { get; set; } = CommentStatus.Normal;
    }
}
