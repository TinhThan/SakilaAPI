using FluentValidation.AspNetCore;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using SakilaAPI.Core;
using SakilaAPI.Core.Authentication;
using SakilaAPI.Core.Exceptions;
using SakilaAPI.Core.Middlewares;
using SakilaAPI.Core.NotifyHub;
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
            var appSetting = Configuration.Get<AppSetting>();
            CurrentOption.AuthenticationString = appSetting.AuthenticationStrings;
            CurrentOption.Endpoints = appSetting.Endpoints;
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
            services.AddHttpClient();

            services.AddSwaggerGen(s =>
            {
                s.SwaggerDoc("v1", new OpenApiInfo { Title = "SakilaAPI", Version = "v1" });
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
            app.UseRouting();
            app.UseMiddleware<AuthenticationMiddleware>();
            app.UseMiddleware<LoggerMiddleware>();

            app.UseCors("AllOrigins");
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
                endpoints.MapHub<NotifycationHub>("/notify");
            });
        }
    }
}
