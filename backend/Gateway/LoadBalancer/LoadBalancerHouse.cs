using Ocelot.Configuration;
using Ocelot.LoadBalancer.LoadBalancers;
using Ocelot.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Gateway.LoadBalancer
{
    public class LoadBalancerHouse : ILoadBalancerHouse
    {
#pragma warning disable 1998
        public async Task<Response<ILoadBalancer>> Get(DownstreamReRoute reRoute, ServiceProviderConfiguration config)
        {
            return new OkResponse<ILoadBalancer>(new CustomLoadBalancer());
        }
#pragma warning restore 1998
    }
}
