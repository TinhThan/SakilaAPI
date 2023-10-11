using FluentValidation.AspNetCore;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using SakilaAPI.Core;
using SakilaAPI.Core.Exceptions;
using SakilaAPI.Core.Middlewares;
using System.Reflection;

namespace SakilaAPI
{
    /// <summary>
    /// Startup
    /// </summary>
    public class Startup
    {
        /// <summary>
        /// Configuration
        /// </summary>
        public IConfiguration Configuration { get; }

        private readonly string AllOrigins = "AllOrigins";

        /// <summary>
        /// config startup application
        /// </summary>
        /// <param name="configuration"></param>
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        /// <summary>
        /// Configuration services
        /// </summary>
        /// <param name="services"></param>
        public void ConfigureServices(IServiceCollection services)
        {
            var connectionString = Configuration.GetConnectionString("Sakila");
            services.AddCore();
            services.AddControllers(options => options.Filters.Add(new ApiExceptionFilterAttribute()));
            //services.AddFluentValidation()
            services.AddEndpointsApiExplorer();

            services.AddSwaggerGen(s =>
            {
                s.SwaggerDoc("v1", new OpenApiInfo { Title = "SakilaAPI", Version = "v1" });
                // Set the comments path for the Swagger JSON and UI.
                var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                s.IncludeXmlComments(xmlPath);
            });

            //Cấu hình cors cho phép tất cả các header và method
            services.AddCors(options =>
            {
                options.AddPolicy(AllOrigins,
                    builder =>
                    {
                        builder.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod();
                    });
            });
        }

        /// <summary>
        /// Configuration application
        /// </summary>
        /// <param name="app"></param>
        /// <param name="environment"></param>
        public void Configure(IApplicationBuilder app, IWebHostEnvironment environment, ILoggerFactory loggerFactory)
        {
            if (environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI(s => s.SwaggerEndpoint("/swagger/v1/swagger.json", "Sakila API v1"));
            }
            loggerFactory.AddLog4Net();
            app.UseHttpsRedirection();
            app.UseMiddleware<RequestResponseMiddleware>();
            app.UseRouting();
            app.UseAuthorization();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
