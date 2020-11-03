using Newtonsoft.Json;
using Ocelot.LoadBalancer.LoadBalancers;
using Ocelot.Middleware;
using Ocelot.Configuration.File;
using Ocelot.Responses;
using Ocelot.Values;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Gateway.LoadBalancer
{
    public class CustomLoadBalancer : ILoadBalancer
    {
#pragma warning disable 1998
        public async Task<Response<ServiceHostAndPort>> Lease(DownstreamContext context)
        {
            var json = System.IO.File.ReadAllText($"./ocelot.{Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT")}.json");

            var fileConfiguration = JsonConvert.DeserializeObject<FileConfiguration>(json);
            var reRoute = fileConfiguration.ReRoutes.FirstOrDefault(x => context.DownstreamReRoute.UpstreamPathTemplate.OriginalValue == x.UpstreamPathTemplate);

            //TODO DownstreamAddresses 중에서, Header에 들어있는 ClientVersion, ProtocolVersion을 읽어서, 유효한 서비스들 중에서 찾아야 한다.
            //TODO 서버간 통신에서는 ProtocolVersion만 검사해야 한다.
            //TODO 해당 기준의 서비스가 하나도 없을 경우, DeveloperException으로 오류코드를 반환해야 한다.

            var count = context.DownstreamReRoute.DownstreamAddresses.Count - 1;
            var address = context.DownstreamReRoute.DownstreamAddresses;
            var serviceHostAndPort = new ServiceHostAndPort(address[count].Host, address[count].Port);
            return new OkResponse<ServiceHostAndPort>(serviceHostAndPort);
        }
#pragma warning restore 1998

        public void Release(ServiceHostAndPort hostAndPort)
        {

        }
    }
}
