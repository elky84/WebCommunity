using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Board.Models;
using Board.Services;
using System.Web;
using EzAspDotNet.Protocols.Page;

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
        public async Task<Protocols.Response.ArticleList> Get(string boardId, [FromQuery] string author,
            [FromQuery] string title, [FromQuery] string content, [FromQuery] Pageable pageable)
        {
            return new Protocols.Response.ArticleList
            {
                Contents = (await _articleService.Get(boardId, author, title, content, pageable)).ConvertAll(x => x.ToListProtocol()),
                Total = await _articleService.CountAsync(boardId)
            };
        }

        [HttpPost("{boardId}")]
        public async Task<Protocols.Response.Article> Create([FromHeader(Name = WebUtil.HeaderKeys.AuthorizedUserId)] string userId,
            [FromHeader(Name = WebUtil.HeaderKeys.AuthorizedNickName)] string encodedNickName,
            string boardId, [FromBody] Protocols.Request.Article article)
        {
            return new Protocols.Response.Article
            {
                Data = (await _articleService.Create(boardId, userId, HttpUtility.UrlDecode(encodedNickName), article))?.ToProtocol()
            };
        }


        [HttpGet("{boardId}/{articleId}")]
        public async Task<Protocols.Response.Article> Get(string boardId, string articleId)
        {
            return new Protocols.Response.Article
            {
                Data = (await _articleService.Get(boardId, articleId))?.ToProtocol()
            };
        }

        [HttpPost("{boardId}/{articleId}/Read")]
        public async Task<Protocols.Response.Article> Read(string boardId, string articleId)
        {
            return new Protocols.Response.Article
            {
                Data = (await _articleService.Read(boardId, articleId))?.ToProtocol()
            };
        }


        [HttpPut("{boardId}/{articleId}")]
        public async Task<Protocols.Response.Article> Update([FromHeader(Name = WebUtil.HeaderKeys.AuthorizedUserId)] string userId,
            string boardId, string articleId, [FromBody] Protocols.Request.Article article)
        {
            return new Protocols.Response.Article
            {
                Data = (await _articleService.Update(boardId, articleId, userId, article))?.ToProtocol()
            };
        }

        [HttpDelete("{boardId}/{articleId}")]
        public async Task<Protocols.Response.Article> Delete([FromHeader(Name = WebUtil.HeaderKeys.AuthorizedUserId)] string userId,
            string boardId, string articleId)
        {
            return new Protocols.Response.Article
            {
                Data = (await _articleService.Delete(boardId, articleId, userId))?.ToProtocol()
            };
        }

        [HttpPost("{boardId}/{articleId}/Recommend")]
        public async Task<Protocols.Response.Article> Recommend([FromHeader(Name = WebUtil.HeaderKeys.AuthorizedUserId)] string userId,
            string boardId, string articleId)
        {
            return new Protocols.Response.Article
            {
                Data = (await _articleService.Recommend(boardId, articleId, userId))?.ToProtocol()
            };
        }

        [HttpPost("{boardId}/{articleId}/NotRecommend")]
        public async Task<Protocols.Response.Article> NotRecommend([FromHeader(Name = WebUtil.HeaderKeys.AuthorizedUserId)] string userId,
            string boardId, string articleId)
        {
            return new Protocols.Response.Article
            {
                Data = (await _articleService.NotRecommend(boardId, articleId, userId))?.ToProtocol()
            };
        }

    }
}
