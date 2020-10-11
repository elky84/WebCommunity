﻿using System.Collections.Generic;
using Web.Models;

namespace Board.Models
{
    public class Article : MongoDbHeader
    {
        public string Title { get; set; }

        public string Content { get; set; }

        public string Author { get; set; }

        public string Category { get; set; }

        public List<string> Tags { get; set; }

        public int Recommend { get; set; }

        public int NotRecommend { get; set; }
    }
}