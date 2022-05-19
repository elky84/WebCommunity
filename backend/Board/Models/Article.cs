using EzMongoDb.Models;
using System.Collections.Generic;

namespace Board.Models
{
    public class Article : MongoDbHeader
    {
        public string Title { get; set; }

        public string Content { get; set; }

        public string UserId { get; set; }

        public string Author { get; set; }

        public string Category { get; set; }

        public List<string> Tags { get; set; }

        public string Source { get; set; }

        public int Recommend { get; set; }

        public int NotRecommend { get; set; }

        public int Hit { get; set; }
    }
}
