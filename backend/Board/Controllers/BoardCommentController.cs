using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Board.Models;
using Board.Services;
using System.Web;
using EzAspDotNet.Protocols.Page;

namespace Board.Controllers
{
    [Route("Board/Comment")]
    [ApiController]
    public class BoardCommentController : ControllerBase
    {
        private readonly CommentService _commentService;

        public BoardCommentController(CommentService commentService)
        {
            _commentService = commentService;
        }

        [HttpGet("{boardId}/{articleId}")]
        public async Task<Protocols.Response.CommentList> Get(string boardId, string articleId, [FromQuery] PageableMini pageable)
        {
            return new Protocols.Response.CommentList
            {
                Datas = (await _commentService.Get(boardId, articleId, pageable)).ConvertAll(x => x.ToProtocol()),
                Total = await _commentService.CountAsync(boardId, articleId)
            };
        }

        [HttpPost("{boardId}/{articleId}")]
        public async Task<Protocols.Response.Comment> Create([FromHeader(Name = WebUtil.HeaderKeys.AuthorizedUserId)] string userId,
            [FromHeader(Name = WebUtil.HeaderKeys.AuthorizedNickName)] string encodedNickName,
            string boardId, string articleId, [FromBody] Protocols.Request.Comment comment)
        {
            return new Protocols.Response.Comment
            {
                Data = (await _commentService.Create(boardId, articleId, userId, HttpUtility.UrlDecode(encodedNickName), comment))?.ToProtocol()
            };
        }

        [HttpPut("{boardId}/{commentId}")]
        public async Task<Protocols.Response.Comment> Update([FromHeader(Name = WebUtil.HeaderKeys.AuthorizedUserId)] string userId,
            string boardId, string commentId, [FromBody] Protocols.Request.Comment comment)
        {
            return new Protocols.Response.Comment
            {
                Data = (await _commentService.Update(boardId, commentId, userId, comment))?.ToProtocol()
            };
        }

        [HttpDelete("{boardId}/{commentId}")]
        public async Task<Protocols.Response.Comment> Delete([FromHeader(Name = WebUtil.HeaderKeys.AuthorizedUserId)] string userId,
            string boardId, string commentId)
        {
            return new Protocols.Response.Comment
            {
                Data = (await _commentService.Delete(boardId, commentId, userId))?.ToProtocol()
            };
        }

        [HttpPost("{boardId}/{commentId}/Recommend")]
        public async Task<Protocols.Response.Comment> Recommend([FromHeader(Name = WebUtil.HeaderKeys.AuthorizedUserId)] string userId,
            string boardId, string commentId)
        {
            return new Protocols.Response.Comment
            {
                Data = (await _commentService.Recommend(boardId, commentId, userId))?.ToProtocol()
            };
        }

        [HttpPost("{boardId}/{commentId}/NotRecommend")]
        public async Task<Protocols.Response.Comment> NotRecommend([FromHeader(Name = WebUtil.HeaderKeys.AuthorizedUserId)] string userId,
            string boardId, string commentId)
        {
            return new Protocols.Response.Comment
            {
                Data = (await _commentService.NotRecommend(boardId, commentId, userId))?.ToProtocol()
            };
        }

    }
}
