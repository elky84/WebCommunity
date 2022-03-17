﻿using EzAspDotNet.Protocols;
using System.Collections.Generic;

namespace Protocols.Common
{
    public class ArticleList : CommonHeader
    {
        public string Title { get; set; }

        public string Author { get; set; }

        public string Category { get; set; }

        public List<string> Tags { get; set; }

        public int Recommend { get; set; }

        public int NotRecommend { get; set; }

        public int Hit { get; set; }
    }
}
