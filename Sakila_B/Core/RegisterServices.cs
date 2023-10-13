using AutoMapper;
using FluentValidation;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Sakila_B.Core.Base.Validator;
using System.Reflection;

namespace Sakila_B.Core
{
    /// <summary>
    /// Đăng ký các service
    /// </summary>
    public static class RegisterServices
    {
        /// <summary>
        /// Đăng ký các service
        /// </summary>
        /// <param name="services"></param>
        /// <returns></returns>
        public static IServiceCollection AddCore(this IServiceCollection services) {
            services.AddMediatR(Assembly.GetExecutingAssembly());
            services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());
            services.AddDbContext<DataContext>();
            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(RequestValidationBehavior<,>));

            #region Configuration Mapper

            var mapperConfig = new MapperConfiguration(x =>
            {
                x.AddProfile(new MappingProfile());
            });

            //Nếu mapper bị lỗi thì throw ra
            mapperConfig.AssertConfigurationIsValid();

            IMapper mapper = mapperConfig.CreateMapper();

            services.AddSingleton(mapper);
            #endregion

            return services;
        }
    }
}
