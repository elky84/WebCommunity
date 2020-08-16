using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Board.Models;
using Board.Services;
using Web.Protocols.Page;

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
        public async Task<Web.Protocols.Response.CommentList> Get(string boardId, string articleId, [FromQuery] PageableMini pageable)
        {
            return new Web.Protocols.Response.CommentList
            {
                Datas = (await _commentService.Get(boardId, articleId, pageable)).ConvertAll(x => x.ToProtocol())
            };
        }

        [HttpPost("{boardId}/{articleId}")]
        public async Task<Web.Protocols.Response.Comment> Create(string boardId, string articleId, [FromBody] Web.Protocols.Request.Comment comment)
        {
            return new Web.Protocols.Response.Comment
            {
                Data = (await _commentService.Create(boardId, articleId, comment))?.ToProtocol()
            };
        }

        [HttpPut("{boardId}/{commentId}")]
        public async Task<Web.Protocols.Response.Comment> Update(string boardId, string commentId, [FromBody] Web.Protocols.Request.Comment comment)
        {
            return new Web.Protocols.Response.Comment
            {
                Data = (await _commentService.Update(boardId, commentId, comment))?.ToProtocol()
            };
        }

        [HttpDelete("{boardId}/{commentId}")]
        public async Task<Web.Protocols.Response.Comment> Delete(string boardId, string articleId, string commentId)
        {
            return new Web.Protocols.Response.Comment
            {
                Data = (await _commentService.Delete(boardId, commentId))?.ToProtocol()
            };
        }

        [HttpPost("{boardId}/{commentId}/Recommend")]
        public async Task<Web.Protocols.Response.Comment> Recommend(string boardId, string articleId, string commentId)
        {
            return new Web.Protocols.Response.Comment
            {
                Data = (await _commentService.Recommend(boardId, commentId))?.ToProtocol()
            };
        }

        [HttpPost("{boardId}/{commentId}/NotRecommend")]
        public async Task<Web.Protocols.Response.Comment> NotRecommend(string boardId, string articleId, string commentId)
        {
            return new Web.Protocols.Response.Comment
            {
                Data = (await _commentService.NotRecommend(boardId, commentId))?.ToProtocol()
            };
        }

    }
}
