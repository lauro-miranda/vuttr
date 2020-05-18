using LM.VUTTR.Api.Extensions;
using LM.VUTTR.Api.Infra.Context;
using LM.VUTTR.Api.Settings;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;

namespace LM.VUTTR.Api
{
    public class Startup
    {
        IConfiguration Configuration { get; }

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration ?? throw new ArgumentNullException(nameof(configuration));
        }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddCors();

            services.AddControllers();

            services.Configure<JTWSettings>(Configuration.GetSection("JTW"));

            services.AddDbContextPool<VUTTRContext>(options => options.UseMySql(Configuration.GetConnectionString("Me")));

            services.ConfigureDependencyInjection();

            services.AddJWTAuthentication(Configuration.GetSection("JTW:Secret").Value);
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseRouting();

            app.UseCors(x => x
                .AllowAnyOrigin()
                .AllowAnyMethod()
                .AllowAnyHeader());

            app.UseAuthentication();

            app.UseAuthorization();

            app.UseEndpoints(configure => configure.MapControllers());
        }
    }
}