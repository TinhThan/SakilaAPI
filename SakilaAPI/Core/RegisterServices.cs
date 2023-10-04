using AutoMapper;
using FluentValidation;
using MediatR;
using Microsoft.EntityFrameworkCore;
using SakilaAPI.Core.Base.Validator;
using System.Reflection;

namespace SakilaAPI.Core
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

<<<<<<< HEAD
            #region Config automapper

            var mappingConfig = new MapperConfiguration(mc =>
            {
                mc.AddProfile(new MappingProfile());
            });

            mappingConfig.AssertConfigurationIsValid();

            IMapper mapper = mappingConfig.CreateMapper();
            services.AddSingleton(mapper);
=======
            #region Configuration Mapper

            var mapperConfig = new MapperConfiguration(x =>
            {
                x.AddProfile(new AutoMapperProfile());
            });

            //Nếu mapper bị lỗi thì throw ra
            mapperConfig.AssertConfigurationIsValid();

            IMapper mapper = mapperConfig.CreateMapper();

            services.AddSingleton(mapper);

>>>>>>> 101f70ef9824a639cdf251d6c3d58e415c903b47
            #endregion

            return services;
        }
    }
}
