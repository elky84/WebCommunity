using Board.Models;
using Board.Services;
using EzAspDotNet.Exception;
using EzAspDotNet.Models;
using EzAspDotNet.Protocols.Page;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web;

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
                Datas = MapperUtil.Map<List<Comment>, List<Protocols.Common.Comment>>(await _commentService.Get(boardId, articleId, pageable)),
                Total = await _commentService.CountAsync(boardId, articleId)
            };
        }

        [HttpPost("{boardId}/{articleId}")]
        public async Task<Protocols.Response.Comment> Create([FromHeader(Name = EzAspDotNet.Constants.HeaderKeys.AuthorizedUserId)] string userId,
            [FromHeader(Name = WebUtil.HeaderKeys.AuthorizedNickName)] string encodedNickName,
            string boardId, string articleId, [FromBody] Protocols.Request.Comment comment)
        {
            var commentModel = await _commentService.Create(boardId, articleId, userId, HttpUtility.UrlDecode(encodedNickName), comment);
            if (commentModel == null)
            {
                throw new DeveloperException(Protocols.Code.ResultCode.NotFoundData);
            }

            return new Protocols.Response.Comment
            {
                Data = MapperUtil.Map<Protocols.Common.Comment>(commentModel)
            };
        }

        [HttpPut("{boardId}/{commentId}")]
        public async Task<Protocols.Response.Comment> Update([FromHeader(Name = EzAspDotNet.Constants.HeaderKeys.AuthorizedUserId)] string userId,
            string boardId, string commentId, [FromBody] Protocols.Request.Comment comment)
        {
            var commentModel = await _commentService.Update(boardId, commentId, userId, comment);
            if (commentModel == null)
            {
                throw new DeveloperException(Protocols.Code.ResultCode.NotFoundData);
            }

            return new Protocols.Response.Comment
            {
                Data = MapperUtil.Map<Protocols.Common.Comment>(commentModel)
            };
        }

        [HttpDelete("{boardId}/{commentId}")]
        public async Task<Protocols.Response.Comment> Delete([FromHeader(Name = EzAspDotNet.Constants.HeaderKeys.AuthorizedUserId)] string userId,
            string boardId, string commentId)
        {
            var commentModel = await _commentService.Delete(boardId, commentId, userId);
            if (commentModel == null)
            {
                throw new DeveloperException(Protocols.Code.ResultCode.NotFoundData);
            }

            return new Protocols.Response.Comment
            {
                Data = MapperUtil.Map<Protocols.Common.Comment>(commentModel)
            };
        }

        [HttpPost("{boardId}/{commentId}/Recommend")]
        public async Task<Protocols.Response.Comment> Recommend([FromHeader(Name = EzAspDotNet.Constants.HeaderKeys.AuthorizedUserId)] string userId,
            string boardId, string commentId)
        {
            var commentModel = await _commentService.Recommend(boardId, commentId, userId);
            if (commentModel == null)
            {
                throw new DeveloperException(Protocols.Code.ResultCode.NotFoundData);
            }

            return new Protocols.Response.Comment
            {
                Data = MapperUtil.Map<Protocols.Common.Comment>(commentModel)
            };
        }

        [HttpPost("{boardId}/{commentId}/NotRecommend")]
        public async Task<Protocols.Response.Comment> NotRecommend([FromHeader(Name = EzAspDotNet.Constants.HeaderKeys.AuthorizedUserId)] string userId,
            string boardId, string commentId)
        {
            var commentModel = await _commentService.NotRecommend(boardId, commentId, userId);
            if (commentModel == null)
            {
                throw new DeveloperException(Protocols.Code.ResultCode.NotFoundData);
            }
            return new Protocols.Response.Comment
            {
                Data = MapperUtil.Map<Protocols.Common.Comment>(commentModel)
            };
        }

    }
}
