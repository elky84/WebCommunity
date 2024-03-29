﻿using EzAspDotNet.StartUp;
using Gateway.Middlewares;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using NLog;
using Ocelot.DependencyInjection;
using Ocelot.LoadBalancer.Middleware;
using Ocelot.Middleware;
using Protocols.Exception;
using System.Net;
using System.Text;

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

            // Add Cors
            services.AddCors(options =>
            {
                options.AddDefaultPolicy(
                    builder => builder
#if DEBUG
                        .WithOrigins("http://localhost:8080")
#endif
                        .AllowAnyMethod()
                        .AllowAnyHeader()
                        .AllowCredentials()
                        .WithExposedHeaders("Set-Cookie"));
            });

            services.AddHttpClient();

            services.AddSingleton<AuthMiddleware>();

            services.AddSingleton<LoadBalancingMiddleware>();

            services.AddOcelot(Configuration);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public async void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.CommonConfigure(env);

            authMiddleware = app.ApplicationServices.GetService<AuthMiddleware>();
            app.UseHttpsRedirection();

            app.UseCors();
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
                        if (e.GetType() == typeof(DeveloperException))
                        {
                            var developerException = (DeveloperException)e;
                            ctx.HttpContext.Response.StatusCode = (int)developerException.HttpStatusCode;

                            await ctx.HttpContext.Response.Body.WriteAsync(Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(new ErrorDetails
                            {
                                StatusCode = ctx.HttpContext.Response.StatusCode,
                                Detail = developerException.Detail,
                                ResultCode = developerException.ResultCode
                            })));
                        }
                        else
                        {
                            ctx.HttpContext.Response.StatusCode = (int)HttpStatusCode.Unauthorized;
                            await ctx.HttpContext.Response.Body.WriteAsync(Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(new ErrorDetails()
                            {
                                StatusCode = ctx.HttpContext.Response.StatusCode,
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
