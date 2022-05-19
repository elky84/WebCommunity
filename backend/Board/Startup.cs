using AutoMapper;
using Board.Services;
using EzAspDotNet.StartUp;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Board
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
            EzAspDotNet.Models.MapperUtil.Initialize(
                new MapperConfiguration(cfg =>
                {
                    cfg.CreateMap<Models.Article, Protocols.Common.ArticleList>();
                    cfg.CreateMap<Protocols.Common.ArticleList, Models.Article>();

                    cfg.CreateMap<Models.Article, Protocols.Common.Article>();
                    cfg.CreateMap<Protocols.Common.Article, Models.Article>();

                    cfg.CreateMap<Models.Comment, Protocols.Common.Comment>();
                    cfg.CreateMap<Protocols.Common.Comment, Models.Comment>();
                })
            );

            services.CommonConfigureServices();

            services.AddSingleton<ArticleService>();
            services.AddSingleton<CommentService>();
            services.AddSingleton<BoardDefinitionService>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.CommonConfigure(env);
        }
    }
}
