﻿using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Board.Models;
using Board.Services;
using Web.Protocols.Page;

namespace Board.Controllers
{
    [Route("Board")]
    [ApiController]
    public class BoardController : ControllerBase
    {
        private readonly ArticleService _articleService;

        public BoardController(ArticleService articleService)
        {
            _articleService = articleService;
        }

        [HttpGet("{boardId}")]
        public async Task<Web.Protocols.Response.ArticleList> Get(string boardId, [FromQuery] string keyword, [FromQuery] Pageable pageable)
        {
            return new Web.Protocols.Response.ArticleList
            {
                Contents = (await _articleService.Get(boardId, keyword, pageable)).ConvertAll(x => x.ToListProtocol()),
                Total = await _articleService.CountAsync(boardId)
            };
        }

        [HttpPost("{boardId}")]
        public async Task<Web.Protocols.Response.Article> Create(string boardId, [FromBody] Web.Protocols.Request.Article article)
        {
            return new Web.Protocols.Response.Article
            {
                Data = (await _articleService.Create(boardId, article))?.ToProtocol()
            };
        }

        [HttpGet("{boardId}/{articleId}")]
        public async Task<Web.Protocols.Response.Article> Get(string boardId, string articleId)
        {
            return new Web.Protocols.Response.Article
            {
                Data = (await _articleService.Get(boardId, articleId))?.ToProtocol()
            };
        }


        [HttpPut("{boardId}/{articleId}")]
        public async Task<Web.Protocols.Response.Article> Update(string boardId, string articleId, [FromBody] Web.Protocols.Request.Article article)
        {
            return new Web.Protocols.Response.Article
            {
                Data = (await _articleService.Update(boardId, articleId, article))?.ToProtocol()
            };
        }

        [HttpDelete("{boardId}/{articleId}")]
        public async Task<Web.Protocols.Response.Article> Delete(string boardId, string articleId)
        {
            return new Web.Protocols.Response.Article
            {
                Data = (await _articleService.Delete(boardId, articleId))?.ToProtocol()
            };
        }

        [HttpPost("{boardId}/{articleId}/Recommend")]
        public async Task<Web.Protocols.Response.Article> Recommend(string boardId, string articleId)
        {
            return new Web.Protocols.Response.Article
            {
                Data = (await _articleService.Recommend(boardId, articleId))?.ToProtocol()
            };
        }

        [HttpPost("{boardId}/{articleId}/NotRecommend")]
        public async Task<Web.Protocols.Response.Article> NotRecommend(string boardId, string articleId)
        {
            return new Web.Protocols.Response.Article
            {
                Data = (await _articleService.NotRecommend(boardId, articleId))?.ToProtocol()
            };
        }

    }
}