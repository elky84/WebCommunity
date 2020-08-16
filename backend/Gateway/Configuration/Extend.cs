using Newtonsoft.Json;
using Ocelot.Configuration.File;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Caching;

namespace Gateway.Configuration
{
    public static class Extend
    {
        public static FileReRoute Get(string cacheKey, string pathTemplate)
        {
            ObjectCache cache = MemoryCache.Default;
            var reRoute = cache[cacheKey] as FileReRoute;
            if (reRoute == null)
            {
                var fileName = $"./ocelot.{Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT")}.json";
                var filePaths = new List<string> { fileName };

                var policy = new CacheItemPolicy();
                policy.ChangeMonitors.Add(new HostFileChangeMonitor(filePaths));

                reRoute = JsonConvert.DeserializeObject<FileConfiguration>(File.ReadAllText(fileName)).ReRoutes
                    .FirstOrDefault(x => x.UpstreamPathTemplate.StartsWith(pathTemplate) && x.DownstreamHostAndPorts.Count() > 0);

                if (reRoute != null)
                {
                    cache.Set(cacheKey, reRoute, policy);
                }
            }
            return reRoute;
        }
    }
}
