using EzAspDotNet.Models;
using System.Collections.Generic;

namespace Board.Models
{
    public class BoardDefinition : MongoDbHeader
    {
        public string BoardId { get; set; }

        public string Title { get; set; }

        public string Type { get; set; }

        public List<string> Categories { get; set; }
    }
}
