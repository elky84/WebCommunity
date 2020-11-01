using Auth.Models.Settings;
using Auth.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using WebUtil.StartUp;

namespace Auth
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

            services.Configure<AppSettings>(Configuration.GetSection(nameof(AppSettings)));

            services.AddSingleton<IAppSettings>(sp =>
                sp.GetRequiredService<IOptions<AppSettings>>().Value);

            services.AddTransient<TokenService>();
            services.AddTransient<AccountService>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.CommonConfigure(env);
        }
    }
}
