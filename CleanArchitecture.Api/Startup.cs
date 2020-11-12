using CleanArchitecture.Application.AppOptions;
using CleanArchitecture.Infra.Data.Context;
using CleanArchitecture.Infra.IoC;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Newtonsoft.Json;

namespace CleanArchitecture.Api
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
            #region Register DbContext

            services.AddDbContext<AppDbContext>(options =>
            {
                options.UseSqlServer(
                    Configuration.GetConnectionString("DefaultConnection"));
            });

            #endregion

            #region Register Controller

            services.AddControllers()
                .AddNewtonsoftJson(o =>
                {
                    o.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
                })
                .AddFluentValidation(o =>
                {
                    o.RegisterValidatorsFromAssemblyContaining<Startup>();
                });

            #endregion

            #region Register CORS

            services.AddCors();

            #endregion

            #region Register Swagger

            services.RegisterSwagger();

            #endregion

            #region Register Dependency

            services.RegisterServices();

            #endregion

            #region Register Jwt

            services.RegisterJwt(Configuration);

            #endregion
        }               

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            var swaggerOptions = new SwaggerSettings();
            Configuration.GetSection(nameof(SwaggerSettings)).Bind(swaggerOptions);
            app.UseSwagger();
            app.UseSwaggerUI(option => 
            { 
                option.SwaggerEndpoint(swaggerOptions.JsonRoute, swaggerOptions.Description); 
            });

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();
            app.UseCors(c => c.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller}/{action=Index}/{id?}");
                endpoints.MapControllers();
            });
        }
    }
}
