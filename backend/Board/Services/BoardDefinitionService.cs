
using EzAspDotNet.Services;
using EzAspDotNet.Util;

namespace Board.Services
{
    public class BoardDefinitionService
    {
        private readonly MongoDbService _mongoDbService;

        private readonly MongoDbUtil<Models.BoardDefinition> _mongoDbBoardDefinition;

        public BoardDefinitionService(MongoDbService mongoDbService)
        {
            _mongoDbService = mongoDbService;
            _mongoDbBoardDefinition = new MongoDbUtil<Models.BoardDefinition>(_mongoDbService.Database);
        }
    }
}
