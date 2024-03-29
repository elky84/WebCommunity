﻿using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Protocols.Code;
using EzAspDotNet.Protocols;

namespace Protocols.Common
{
    public class Comment : CommonHeader
    {
        public string ArticleId { get; set; }

        public string CommentId { get; set; }

        public string Content { get; set; }

        public string Author { get; set; }

        public string UserId { get; set; }

        public string OriginAuthor { get; set; }

        public int Recommend { get; set; }

        public int NotRecommend { get; set; }

        [JsonConverter(typeof(StringEnumConverter))]
        public CommentStatus Status { get; set; } = CommentStatus.Normal;
    }
}
