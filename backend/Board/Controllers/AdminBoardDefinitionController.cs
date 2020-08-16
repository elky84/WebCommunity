using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Board.Models;
using Board.Services;
using Web.Protocols;
using Web.Types;

namespace Board.Controllers
{
    [Route("Board/Admin/BoardDefinition")]
    [ApiController]
    public class AdminBoardDefinitinoController : ControllerBase
    {
        private readonly ArticleService _boardService;

        public AdminBoardDefinitinoController(ArticleService boardService)
        {
            _boardService = boardService;
        }


    }
}
