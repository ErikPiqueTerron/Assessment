using Assessment.Business.Impl.Module;
using Assessment.Infrastructure.Impl;
using Assessment.Infrastructure.Impl.Module;
using Assessment.WebApi.Authentication;
using Assessment.WebApi.Filters;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Swashbuckle.AspNetCore.Swagger;
using System;
using System.IO;
using System.Reflection;

namespace Assessment.WebApi
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
            services.AddCors()
                .AddMemoryCache()
                .AddMvc(options => options.Filters.Add<GlobalExceptionFilter>())
                .Services
                .AddSwaggerGen(config =>
                {
                    config.SwaggerDoc("v1", new Info
                    {
                        Title = Configuration.GetSection("settings:swagger:title").Value,
                        Version = Configuration.GetSection("settings:swagger:version").Value,
                        Description = Configuration.GetSection("settings:swagger:description").Value
                    });

                    var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                    var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);

                    config.IncludeXmlComments(xmlPath);
                })
                .AddLogging()
                .AddBusinessServices()
                .AddInfrastructureServices();

            services.AddAuthentication(BasicAuthenticationDefaults.AuthenticationScheme)
                .AddBasic<HttpProxy>(BasicAuthenticationDefaults.AuthenticationScheme, options => options.Realm = "Assessment");

            services.AddAuthorization(options =>
            {
                options.AddPolicy("AllRoles", policy => policy.RequireRole("admin", "user"));
                options.AddPolicy("AdminRole", policy => policy.RequireRole("admin"));
            });
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseStaticFiles()
                    .UseSwagger()
                    .UseSwaggerUI(config =>
                    {
                        config.SwaggerEndpoint(Configuration.GetSection("settings:swagger:endpoint").Value, "Assessment API V1");
                    });
            }

            app.UseAuthentication()
                .UseMvc();
        }
    }
}
