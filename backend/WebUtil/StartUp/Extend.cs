using WebUtil.Filter;
using Microsoft.Extensions.DependencyInjection;
using NLog;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Builder;
using WebUtil.Exception;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Hosting;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Microsoft.OpenApi.Models;
using WebUtil.Swagger;
using WebUtil.Service;

namespace WebUtil.StartUp
{
    public static class Extend
    {
        private static readonly Logger logger = LogManager.GetCurrentClassLogger();

        public static void CommonConfigureServices(this IServiceCollection services)
        {
            services.AddHttpClient();

            services.AddTransient<MongoDbService>();

            services.Configure<CookiePolicyOptions>(options =>
            {
                options.MinimumSameSitePolicy = SameSiteMode.None;
                options.Secure = CookieSecurePolicy.Always;
            });

            services.AddMvc(option => option.EnableEndpointRouting = false)
                .AddNewtonsoftJson(options =>
                {
                    // Return JSON responses in LowerCase?
                    options.SerializerSettings.ContractResolver = new CustomJsonResolver();

                    // Resolve Looping navigation properties
                    options.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
                }).SetCompatibilityVersion(CompatibilityVersion.Version_3_0);

            services.AddSwaggerGen(c =>
                    {
                        c.SwaggerDoc("v1", new OpenApiInfo { Title = "API", Version = "v1" });
                        c.SchemaFilter<SwaggerExcludeFilter>();
                    });

            services.ConfigureSwaggerGen(options =>
            {
                options.CustomSchemaIds(x => x.FullName);
            });

            services.Configure<ApiBehaviorOptions>(options =>
            {
                options.SuppressModelStateInvalidFilter = true;
            });

            services.AddMvcCore(options =>
            {
                options.Filters.Add(typeof(ValidateModelFilter));
            });
        }

        public static void CommonConfigure(this IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.ConfigureExceptionHandler(logger);

            app.UseCookiePolicy();

            app.UseMvc();

            app.UseSwagger(c =>
            {
                c.RouteTemplate = "api-docs/{documentName}/swagger.json";
            });

            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/api-docs/v1/swagger.json", "My API V1");
            });
        }
    }
}
