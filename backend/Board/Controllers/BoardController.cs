﻿using Board.Models;
using Board.Services;
using EzAspDotNet.Exception;
using EzAspDotNet.Models;
using EzAspDotNet.Protocols.Page;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web;
using WebUtil.Constants;

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
                Contents = MapperUtil.Map<List<Article>, List<Protocols.Common.ArticleList>>(await _articleService.Get(boardId, author, title, content, pageable)),
                Total = await _articleService.CountAsync(boardId),
                Pageable = pageable,
            };
        }

        [HttpPost("{boardId}")]
        public async Task<Protocols.Response.Article> Create([FromHeader(Name = EzAspDotNet.Constants.HeaderKeys.AuthorizedUserId)] string userId,
            [FromHeader(Name = HeaderKeys.AuthorizedNickName)] string encodedNickName,
            string boardId, [FromBody] Protocols.Request.Article article)
        {
            var articleModel = await _articleService.Create(boardId, userId, HttpUtility.UrlDecode(encodedNickName), article);
            if (articleModel == null)
            {
                throw new DeveloperException(Protocols.Code.ResultCode.NotFoundData);
            }

            return new Protocols.Response.Article
            {
                Data = MapperUtil.Map<Protocols.Common.Article>(articleModel),
            };
        }


        [HttpGet("{boardId}/{articleId}")]
        public async Task<Protocols.Response.Article> Get(string boardId, string articleId)
        {
            var articleModel = await _articleService.Get(boardId, articleId);
            if (articleModel == null)
            {
                throw new DeveloperException(Protocols.Code.ResultCode.NotFoundData);
            }

            return new Protocols.Response.Article
            {
                Data = MapperUtil.Map<Protocols.Common.Article>(articleModel),
            };
        }

        [HttpPost("{boardId}/{articleId}/Read")]
        public async Task<Protocols.Response.Article> Read(string boardId, string articleId)
        {
            var articleModel = await _articleService.Read(boardId, articleId);
            if (articleModel == null)
            {
                throw new DeveloperException(Protocols.Code.ResultCode.NotFoundData);
            }

            return new Protocols.Response.Article
            {
                Data = MapperUtil.Map<Protocols.Common.Article>(articleModel),
            };
        }


        [HttpPut("{boardId}/{articleId}")]
        public async Task<Protocols.Response.Article> Update([FromHeader(Name = EzAspDotNet.Constants.HeaderKeys.AuthorizedUserId)] string userId,
            string boardId, string articleId, [FromBody] Protocols.Request.Article article)
        {
            var articleModel = await _articleService.Update(boardId, articleId, userId, article);
            if (articleModel == null)
            {
                throw new DeveloperException(Protocols.Code.ResultCode.NotFoundData);
            }

            return new Protocols.Response.Article
            {
                Data = MapperUtil.Map<Protocols.Common.Article>(articleModel),
            };
        }

        [HttpDelete("{boardId}/{articleId}")]
        public async Task<Protocols.Response.Article> Delete([FromHeader(Name = EzAspDotNet.Constants.HeaderKeys.AuthorizedUserId)] string userId,
            string boardId, string articleId)
        {
            var articleModel = await _articleService.Delete(boardId, articleId, userId);
            if (articleModel == null)
            {
                throw new DeveloperException(Protocols.Code.ResultCode.NotFoundData);
            }

            return new Protocols.Response.Article
            {
                Data = MapperUtil.Map<Protocols.Common.Article>(articleModel),
            };
        }

        [HttpPost("{boardId}/{articleId}/Recommend")]
        public async Task<Protocols.Response.Article> Recommend([FromHeader(Name = EzAspDotNet.Constants.HeaderKeys.AuthorizedUserId)] string userId,
            string boardId, string articleId)
        {
            var articleModel = await _articleService.Recommend(boardId, articleId, userId);
            if (articleModel == null)
            {
                throw new DeveloperException(Protocols.Code.ResultCode.NotFoundData);
            }

            return new Protocols.Response.Article
            {
                Data = MapperUtil.Map<Protocols.Common.Article>(articleModel),
            };
        }

        [HttpPost("{boardId}/{articleId}/NotRecommend")]
        public async Task<Protocols.Response.Article> NotRecommend([FromHeader(Name = EzAspDotNet.Constants.HeaderKeys.AuthorizedUserId)] string userId,
            string boardId, string articleId)
        {
            var articleModel = await _articleService.NotRecommend(boardId, articleId, userId);
            if (articleModel == null)
            {
                throw new DeveloperException(Protocols.Code.ResultCode.NotFoundData);
            }

            return new Protocols.Response.Article
            {
                Data = MapperUtil.Map<Protocols.Common.Article>(articleModel),
            };
        }

    }
}
