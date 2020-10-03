using Board.Models;
using MongoDB.Driver;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Threading.Tasks;
using Web.Protocols.Page;
using WebUtil.Service;
using WebUtil.Util;

namespace Board.Services
{
    public class ArticleService
    {
        private MongoDbService _mongoDbService;

        private readonly ConcurrentDictionary<string, MongoDbUtil<Article>> _mongoDbBoards = new ConcurrentDictionary<string, MongoDbUtil<Article>>();

        public ArticleService(MongoDbService mongoDbService)
        {
            _mongoDbService = mongoDbService;
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

        public async Task<List<Article>> Get(string id, string keyword, Pageable pageable)
        {
            var mongoDbUtil = GetMongoDbBoard(id);

            var builder = Builders<Article>.Filter;
            var filter = FilterDefinition<Article>.Empty;
            if (!string.IsNullOrEmpty(keyword))
            {
                filter &= builder.Regex(x => x.Title, ".*" + keyword + ".*");
            }

            return await mongoDbUtil.Page(filter, pageable.Limit, pageable.Offset, pageable.Sort, pageable.Asc);
        }

        public async Task<Article> Create(string id, Web.Protocols.Request.Article board)
        {
            var mongoDbUtil = GetMongoDbBoard(id);
            return await mongoDbUtil.CreateAsync(board.ToModel());
        }

        public async Task<Article> Update(string id, string articleId, Web.Protocols.Request.Article board)
        {
            var mongoDbUtil = GetMongoDbBoard(id);
            return await mongoDbUtil.UpdateAsync(articleId, board.ToModel());
        }

        public async Task<Article> Recommend(string id, string articleId)
        {
            var mongoDbUtil = GetMongoDbBoard(id);
            return await mongoDbUtil.UpdateGetAsync(articleId, Builders<Article>.Update.Inc(x => x.Recommend, 1));
        }

        public async Task<Article> NotRecommend(string id, string articleId)
        {
            var mongoDbUtil = GetMongoDbBoard(id);
            return await mongoDbUtil.UpdateGetAsync(articleId, Builders<Article>.Update.Inc(x => x.NotRecommend, 1));
        }


        public async Task<Article> Delete(string id, string articleId)
        {
            var mongoDbUtil = GetMongoDbBoard(id);
            return await mongoDbUtil.RemoveAsync(articleId);
        }
    }
}
