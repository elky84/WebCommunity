using Board.Models;
using MongoDB.Driver;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Threading.Tasks;
using Protocols.Exception;
using WebUtil.Service;
using WebUtil.Util;
using EzAspDotNet.Protocols.Page;

namespace Board.Services
{
    public class ArticleService
    {
        private MongoDbService _mongoDbService;

        private readonly ConcurrentDictionary<string, MongoDbUtil<Article>> _mongoDbBoards = new ConcurrentDictionary<string, MongoDbUtil<Article>>();

        private CommentService _commentService;

        public ArticleService(MongoDbService mongoDbService,
            CommentService commentService)
        {
            _mongoDbService = mongoDbService;
            _commentService = commentService;
        }


        private MongoDbUtil<Article> GetMongoDbBoard(string id)
        {
            if (_mongoDbBoards.TryGetValue(id, out var value))
            {
                return value;
            }

            var newData = new MongoDbUtil<Article>(_mongoDbService.Database, id);
            _mongoDbBoards.TryAdd(id, newData);
            return newData;
        }

        public async Task<long> CountAsync(string id)
        {
            var mongoDbUtil = GetMongoDbBoard(id);
            return await mongoDbUtil.CountAsync();
        }

        public async Task<List<Article>> Get(string id, string author, string title, string content, Pageable pageable)
        {
            var mongoDbUtil = GetMongoDbBoard(id);

            var builder = Builders<Article>.Filter;
            var filter = FilterDefinition<Article>.Empty;
            if (!string.IsNullOrEmpty(author))
            {
                filter &= builder.Regex(x => x.Author, ".*" + author + ".*");
            }

            if (!string.IsNullOrEmpty(title))
            {
                filter &= builder.Regex(x => x.Title, ".*" + title + ".*");
            }

            if (!string.IsNullOrEmpty(content))
            {
                filter &= builder.Regex(x => x.Content, ".*" + content + ".*");
            }

            return await mongoDbUtil.Page(filter, pageable.Limit, pageable.Offset, pageable.Sort, pageable.Asc);
        }

        public async Task<Article> Create(string id, string userId, string nickName, Protocols.Request.Article article)
        {
            var mongoDbUtil = GetMongoDbBoard(id);
            return await mongoDbUtil.CreateAsync(article.ToModel(userId, nickName));
        }

        public async Task<Article> Get(string id, string articleId)
        {
            var mongoDbUtil = GetMongoDbBoard(id);
            return await mongoDbUtil.FindOneAsyncById(articleId);
        }

        public async Task<Article> Read(string id, string articleId)
        {
            var mongoDbUtil = GetMongoDbBoard(id);
            return await mongoDbUtil.UpdateGetAsync(articleId, Builders<Article>.Update.Inc(x => x.Hit, 1));
        }

        public async Task<Article> Update(string id, string articleId, string userId, Protocols.Request.Article article)
        {
            var mongoDbUtil = GetMongoDbBoard(id);
            var origin = await GetAndValidation(mongoDbUtil, articleId, userId);
            await mongoDbUtil.UpdateAsync(articleId,
                Builders<Article>.Update.Set(x => x.Tags, article.Tags)
                .Set(x => x.Title, article.Title)
                .Set(x => x.Content, article.Content)
                .Set(x => x.Category, article.Category));

            return origin;
        }

        public async Task<Article> Recommend(string id, string articleId, string userId)
        {
            var mongoDbUtil = GetMongoDbBoard(id);
            return await mongoDbUtil.UpdateGetAsync(articleId, Builders<Article>.Update.Inc(x => x.Recommend, 1));
        }

        public async Task<Article> NotRecommend(string id, string articleId, string userId)
        {
            var mongoDbUtil = GetMongoDbBoard(id);
            return await mongoDbUtil.UpdateGetAsync(articleId, Builders<Article>.Update.Inc(x => x.NotRecommend, 1));
        }


        public async Task<Article> Delete(string id, string articleId, string userId)
        {
            var mongoDbUtil = GetMongoDbBoard(id);
            var origin = await GetAndValidation(mongoDbUtil, articleId, userId);
            await mongoDbUtil.RemoveAsync(articleId);
            await _commentService.DeleteByArticleId(id, articleId);
            return origin;
        }


        private async Task<Article> GetAndValidation(MongoDbUtil<Article> mongoDbUtil, string articleId, string userId)
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

            return origin;
        }

    }
}
