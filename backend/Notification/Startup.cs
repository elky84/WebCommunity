using System;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using EzAspDotNet.WebSocketManager;
using EzAspDotNet.StartUp;
using Microsoft.Extensions.Hosting;
using EzAspDotNet.Service;

namespace Notification
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.CommonConfigureServices();

            services.AddWebSocketManager();

            services.AddSingleton<IHostedService, RabbitMQ.ScheduledService>();
        }


        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, IServiceProvider serviceProvider)
        {
            app.CommonConfigure(env);

            app.UseWebSockets();

            app.MapWebSocketManager("/ws", serviceProvider.GetService<WebSocket.Handler>());

            app.UseFileServer();
        }
    }
}