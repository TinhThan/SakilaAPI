using FluentValidation.AspNetCore;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using Sakila_B.Core;
using Sakila_B.Core.Exceptions;
using Sakila_B.Core.Middlewares;
using System.Reflection;

namespace Sakila_B
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
            services.AddCore();
            services.AddControllers(options => options.Filters.Add(new ApiExceptionFilterAttribute()));
            services.AddEndpointsApiExplorer();

            services.AddSwaggerGen(s =>
            {
                s.SwaggerDoc("v1", new OpenApiInfo { Title = "Sakila_B", Version = "v1" });
                // Set the comments path for the Swagger JSON and UI.
                var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                s.IncludeXmlComments(xmlPath);

                // add form authentication for swagger 
                s.AddSecurityDefinition("PrivateKey", new OpenApiSecurityScheme
                {
                    In = ParameterLocation.Header,
                    Description = "Please enter a valid token",
                    Name = "Secret-Key",
                    Type = SecuritySchemeType.ApiKey,
                    Scheme = "Secret-Key"
                });
                var scheme = new OpenApiSecurityScheme
                {
                    Reference = new OpenApiReference
                    {
                        Type = ReferenceType.SecurityScheme,
                        Id = "PrivateKey"
                    },
                    In = ParameterLocation.Header
                };
                var requirement = new OpenApiSecurityRequirement
                {
                    {scheme, new List<string>()}
                };
                s.AddSecurityRequirement(requirement);
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

            //Use Auth with middleware
            app.UseMiddleware<LoggerMiddleware>();
            app.UseRouting();
            app.UseAuthorization();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
