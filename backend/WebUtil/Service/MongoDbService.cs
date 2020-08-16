using Microsoft.Extensions.Configuration;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using WebUtil.Settings;

namespace WebUtil.Service
{
    public class MongoDbService
    {
        public IMongoDatabase Database { get; private set; }

        public bool IsInitialized { get; set; } = false;

        public MongoDbService(IConfiguration configuration)
        {
            var client = new MongoClient(configuration.GetMongoDbSettings().ConnectionString);
            Database = client.GetDatabase(configuration.GetMongoDbSettings().DatabaseName);
        }
    }
}
