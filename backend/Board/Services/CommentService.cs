using Board.Models;
using MongoDB.Driver;
using System.Collections.Generic;
using System.Threading.Tasks;
using Protocols.Exception;
using EzAspDotNet.Protocols.Page;
using EzAspDotNet.Services;
using EzAspDotNet.Util;

namespace Board.Services
{
    public class CommentService
    {
        private readonly MongoDbService _mongoDbService;

        private readonly Dictionary<string, MongoDbUtil<Comment>> _mongoDbBoardComments = new Dictionary<string, MongoDbUtil<Comment>>();

        public CommentService(MongoDbService mongoDbService)
        {
            _mongoDbService = mongoDbService;
        }


        private MongoDbUtil<Comment> GetMongoDbBoardComment(string id)
        {
            var commentCollectionId = $"{id}Comment";
            if (_mongoDbBoardComments.TryGetValue(commentCollectionId, out var value))
            {
                return value;
            }

            var newData = new MongoDbUtil<Comment>(_mongoDbService.Database, commentCollectionId);
            _mongoDbBoardComments.TryAdd(commentCollectionId, newData);
            return newData;
        }

        public async Task<List<Comment>> Get(string id, string articleId, PageableMini pageable)
        {
            var mongoDbUtil = GetMongoDbBoardComment(id);
            return await mongoDbUtil.Page(Builders<Comment>.Filter.Eq(x => x.ArticleId, articleId), pageable.Limit, pageable.Offset,
                new Dictionary<string, int>
                {
                    {"CommentId", 1},
                    {"Created", 1 }
                });
        }

        public async Task<long> CountAsync(string id, string articleId)
        {
            var mongoDbUtil = GetMongoDbBoardComment(id);
            return await mongoDbUtil.CountAsync(Builders<Comment>.Filter.Eq(x => x.ArticleId, articleId));
        }

        public async Task<Comment> Create(string id, string articleId, string userId, string nickName, Protocols.Request.Comment comment)
        {
            var mongoDbUtil = GetMongoDbBoardComment(id);
            var originComment = string.IsNullOrEmpty(comment.OriginCommentId) ? null : await mongoDbUtil.FindOneAsyncById(comment.OriginCommentId);

            var created = await mongoDbUtil.CreateAsync(comment.ToModel(articleId, userId, nickName, originComment?.CommentId, originComment?.Author));
            if (created != null && string.IsNullOrEmpty(comment.OriginCommentId))
            {
                await mongoDbUtil.UpdateAsync(created.Id, Builders<Comment>.Update.Set(x => x.CommentId, created.Id));
            }

            return created;
        }

        public async Task<Comment> Update(string id, string commentId, string userId, Protocols.Request.Comment comment)
        {
            var mongoDbUtil = GetMongoDbBoardComment(id);

            var origin = await GetAndValidation(mongoDbUtil, commentId, userId);

            var result = await mongoDbUtil.UpdateAsync(commentId, Builders<Comment>.Update.Set(x => x.Content, comment.Content));
            if (!result)
            {
                throw new DeveloperException(Protocols.Code.ResultCode.DatabaseUpdateFailure);
            }

            origin.Content = comment.Content;
            return origin;
        }

        private async Task<Comment> GetAndValidation(MongoDbUtil<Comment> mongoDbUtil, string articleId, string userId)
        {
            var origin = await mongoDbUtil.FindOneAsyncById(articleId);
            if (origin == null)
            {
                throw new DeveloperException(Protocols.Code.ResultCode.NotFoundData);
            }

            if (origin.UserId != userId)
            {
                throw new DeveloperException(Protocols.Code.ResultCode.NotMatchedAuthor);
            }

            if (origin.Status != Protocols.Code.CommentStatus.Normal)
            {
                throw new DeveloperException(Protocols.Code.ResultCode.NotEditComment);
            }

            return origin;
        }

        public async Task<Comment> Recommend(string id, string commentId, string userId)
        {
            var mongoDbUtil = GetMongoDbBoardComment(id);
            var origin = await GetAndValidation(mongoDbUtil, commentId, userId);
            origin.Recommend += 1;

            await mongoDbUtil.UpdateAsync(commentId, Builders<Comment>.Update.Inc(x => x.Recommend, 1));
            return origin;
        }

        public async Task<Comment> NotRecommend(string id, string commentId, string userId)
        {
            var mongoDbUtil = GetMongoDbBoardComment(id);
            var origin = await GetAndValidation(mongoDbUtil, commentId, userId);
            origin.NotRecommend += 1;

            await mongoDbUtil.UpdateAsync(commentId, Builders<Comment>.Update.Inc(x => x.NotRecommend, 1));
            return origin;
        }

        public async Task<Comment> Delete(string id, string commentId, string userId)
        {
            var mongoDbUtil = GetMongoDbBoardComment(id);
            var origin = await GetAndValidation(mongoDbUtil, commentId, userId);
            origin.Content = "삭제된 댓글입니다."; // RemoveAsync를 하면 댓글 자체가 삭제 되는데, 어떤 글에 대한 대댓글인지에 대한 인지 이슈가 있어, 내용만 삭제한다.
            origin.Status = Protocols.Code.CommentStatus.Deleted;

            await mongoDbUtil.UpdateAsync(origin.Id, origin);
            return origin;
        }

        public async Task DeleteByArticleId(string id, string articleId)
        {
            var mongoDbUtil = GetMongoDbBoardComment(id);
            await mongoDbUtil.RemoveManyAsync(Builders<Comment>.Filter.Eq(x => x.ArticleId, articleId));
        }
    }
}
