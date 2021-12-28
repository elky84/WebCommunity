using Microsoft.AspNetCore.Mvc;
using Board.Services;

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
