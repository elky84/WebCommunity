using RabbitMQ.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EzAspDotNet.Models;

namespace Board.Models
{
    public static class ModelsExtend
    {
        public static Article ToModel(this Protocols.Request.Article article, string userId, string author)
        {
            return new Article
            {
                Id = article.Id,
                UserId = userId,
                Author = author,
                Category = article.Category,
                Content = article.Content,
                Title = article.Title,
                Tags = article.Tags,
                Source = article.Source
            };
        }

        public static Protocols.Common.Article ToProtocol(this Article article)
        {
            return new Protocols.Common.Article
            {
                Author = article.Author,
                UserId = article.UserId,
                Category = article.Category,
                Content = article.Content,
                Title = article.Title,
                Tags = article.Tags,
                Recommend = article.Recommend,
                NotRecommend = article.NotRecommend,
                Source = article.Source,
                Hit = article.Hit
            }.ToProtocol(article);
        }

        public static Protocols.Common.ArticleList ToListProtocol(this Article article)
        {
            return new Protocols.Common.ArticleList
            {
                Author = article.Author,
                Category = article.Category,
                Title = article.Title,
                Tags = article.Tags,
                Recommend = article.Recommend,
                NotRecommend = article.NotRecommend,
                Hit = article.Hit
            }.ToProtocol(article);
        }

        public static Comment ToModel(this Protocols.Request.Comment comment, string articleId, string userId, string author, string commentId = null, string originAuthor = null)
        {
            return new Comment
            {
                UserId = userId,
                Author = author,
                ArticleId = articleId,
                Content = comment.Content,
                CommentId = commentId,
                OriginAuthor = originAuthor
            };
        }

        public static Protocols.Common.Comment ToProtocol(this Comment comment)
        {
            return new Protocols.Common.Comment
            {
                Author = comment.Author,
                UserId = comment.UserId,
                ArticleId = comment.ArticleId,
                CommentId = comment.CommentId,
                Content = comment.Content,
                Recommend = comment.Recommend,
                NotRecommend = comment.NotRecommend,
                OriginAuthor = comment.OriginAuthor,
                Status = comment.Status
            }.ToProtocol(comment);
        }
    }
}
