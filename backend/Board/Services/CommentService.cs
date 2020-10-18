using Board.Models;
using MongoDB.Driver;
using System.Collections.Generic;
using System.Threading.Tasks;
using Web.Protocols.Page;
using WebUtil.Service;
using WebUtil.Util;

namespace Board.Services
{
    public class CommentService
    {
        private MongoDbService _mongoDbService;

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

        public async Task<Comment> Create(string id, string articleId, Web.Protocols.Request.Comment comment)
        {
            var mongoDbUtil = GetMongoDbBoardComment(id);
            var originComment = await mongoDbUtil.FindOneAsyncById(comment.OriginCommentId);

            var created = await mongoDbUtil.CreateAsync(comment.ToModel(articleId, originComment?.CommentId, originComment?.Author));
            if (created != null && string.IsNullOrEmpty(comment.OriginCommentId))
            {
                await mongoDbUtil.UpdateAsync(created.Id, Builders<Comment>.Update.Set(x => x.CommentId, created.Id));
            }

            return created;
        }

        public async Task<Comment> Update(string id, string articleId, Web.Protocols.Request.Comment comment)
        {
            var mongoDbUtil = GetMongoDbBoardComment(id);
            return await mongoDbUtil.UpdateGetAsync(articleId, Builders<Comment>.Update.Set(x => x.Content, comment.Content));
        }

        public async Task<Comment> Recommend(string id, string commentId)
        {
            var mongoDbUtil = GetMongoDbBoardComment(id);
            return await mongoDbUtil.UpdateGetAsync(commentId, Builders<Comment>.Update.Inc(x => x.Recommend, 1));
        }

        public async Task<Comment> NotRecommend(string id, string commentId)
        {
            var mongoDbUtil = GetMongoDbBoardComment(id);
            return await mongoDbUtil.UpdateGetAsync(commentId, Builders<Comment>.Update.Inc(x => x.NotRecommend, 1));
        }

        public async Task<Comment> Delete(string id, string commentId)
        {
            var mongoDbUtil = GetMongoDbBoardComment(id);
            return await mongoDbUtil.RemoveAsync(commentId);
        }
    }
}
