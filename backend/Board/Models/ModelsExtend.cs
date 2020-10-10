using RabbitMQ.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Web.Models;

namespace Board.Models
{
    public static class ModelsExtend
    {
        public static Article ToModel(this Web.Protocols.Request.Article article)
        {
            return new Article
            {
                Id = article.Id,
                Author = article.Author,
                Category = article.Category,
                Content = article.Content,
                Title = article.Title,
                Tags = article.Tags
            };
        }

        public static Web.Protocols.Common.Article ToProtocol(this Article article)
        {
            return new Web.Protocols.Common.Article
            {
                Author = article.Author,
                Category = article.Category,
                Content = article.Content,
                Title = article.Title,
                Tags = article.Tags,
                Recommend = article.Recommend,
                NotRecommend = article.NotRecommend,
            }.ToProtocol(article);
        }

        public static Web.Protocols.Common.ArticleList ToListProtocol(this Article article)
        {
            return new Web.Protocols.Common.ArticleList
            {
                Author = article.Author,
                Category = article.Category,
                Title = article.Title,
                Tags = article.Tags,
                Recommend = article.Recommend,
                NotRecommend = article.NotRecommend,
            }.ToProtocol(article);
        }

        public static Comment ToModel(this Web.Protocols.Request.Comment comment, string articleId)
        {
            return new Comment
            {
                Author = comment.Author,
                ArticleId = articleId,
                CommentId = comment.OriginCommentId,
                Content = comment.Content
            };
        }

        public static Web.Protocols.Common.Comment ToProtocol(this Comment comment)
        {
            return new Web.Protocols.Common.Comment
            {
                Author = comment.Author,
                ArticleId = comment.ArticleId,
                CommentId = comment.CommentId,
                Content = comment.Content,
                Recommend = comment.Recommend,
                NotRecommend = comment.NotRecommend,
            }.ToProtocol(comment);
        }
    }
}
