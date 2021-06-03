using DigichList.Backend.Helpers;
using DigichList.Backend.Options;
using DigichList.Core.Repositories;
using DigichList.Infrastructure.Data;
using DigichList.Infrastructure.Repositories;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
namespace DigichList.Backend
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }
        
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers().AddNewtonsoftJson();
            services.AddLogging(configure => configure.AddConsole());
            services.AddScoped<IDefectImageRepository, DefectImageRepository>();
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IDefectRepository, DefectRepository>();
            services.AddScoped<IRoleRepository, RoleRepository>();
            services.AddScoped<IAdminRepositury, AdminRepository>();
            services.AddScoped<JwtService>();
            services.AddDbContext<DigichListContext>();

            var authOptionsCifiguration = Configuration.GetSection("Auth");
            services.Configure<AuthOptions>(authOptionsCifiguration);

            services.AddCors(oprions =>
            {
                oprions.AddDefaultPolicy(
                    builder =>
                    {
                        builder.AllowAnyOrigin()
                        .AllowAnyMethod()
                        .AllowAnyHeader();
                    });
            });
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
