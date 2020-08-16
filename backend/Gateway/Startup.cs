﻿using System.Net;
using System.Text;
using Gateway.Middlewares;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using NLog;
using Ocelot.DependencyInjection;
using Ocelot.LoadBalancer.LoadBalancers;
using Ocelot.LoadBalancer.Middleware;
using Ocelot.Middleware;
using Web.Protocols.Exception;
using WebUtil.StartUp;

namespace Gateway
{
    public class Startup
    {
        private static readonly Logger logger = LogManager.GetCurrentClassLogger();

        private AuthMiddleware authMiddleware;

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.CommonConfigureServices();

            services.AddCors(options => options.AddPolicy("AllowSpecificOrigin",
                builder =>
                {
                    builder.AllowAnyOrigin()
                        .AllowAnyHeader()
                        .AllowAnyMethod();
                }));

            services.AddHttpClient();

            services.AddSingleton<AuthMiddleware>();

            services.AddSingleton<LoadBalancingMiddleware>();
            services.AddSingleton<ILoadBalancerHouse, LoadBalancer.LoadBalancerHouse>();

            services.AddOcelot(Configuration);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public async void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.CommonConfigure(env);

            authMiddleware = app.ApplicationServices.GetService<AuthMiddleware>();

            //TODO https 로컬 환경 때문에 검토 필요.
            //if (!env.IsDevelopment())
            //{
            //    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
            //    app.UseHsts();
            //}
            //app.UseHttpsRedirection();

            app.UseCors("AllowSpecificOrigin");
            app.UseWebSockets();

            var ocelotConfiguration = new OcelotPipelineConfiguration
            {
                PreAuthenticationMiddleware = async (ctx, next) =>
                {
                    try
                    {
                        await authMiddleware.Authenticate(ctx, next);
                    }
                    catch (System.Exception e)
                    {
                        if (e.GetType() == typeof(LogicException))
                        {
                            var logicException = (LogicException)e;
                            ctx.HttpContext.Response.StatusCode = (int)logicException.HttpStatusCode;

                            await ctx.HttpContext.Response.Body.WriteAsync(Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(new ErrorDetails
                            {
                                StatusCode = ctx.HttpContext.Response.StatusCode,
                                ErrorMessage = logicException.ResultCode.ToString(),
                                Detail = logicException.Detail,
                                ResultCode = logicException.ResultCode
                            })));
                        }
                        else
                        {
                            ctx.HttpContext.Response.StatusCode = (int)HttpStatusCode.Unauthorized;
                            await ctx.HttpContext.Response.Body.WriteAsync(Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(new ErrorDetails()
                            {
                                StatusCode = ctx.HttpContext.Response.StatusCode,
                                ErrorMessage = ctx.HttpContext.Response.StatusCode.ToString(),
                                Detail = e.Message
                            })));
                        }

                        logger.Error(e.Message);
                    }
                }
            };

            await OcelotMiddlewareExtensions.UseOcelot(app, ocelotConfiguration);
        }
    }
}
