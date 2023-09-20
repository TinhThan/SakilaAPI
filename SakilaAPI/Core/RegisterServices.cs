using Microsoft.EntityFrameworkCore;

namespace SakilaAPI.Core
{
    public static class RegisterServices
    {
        public static IServiceCollection AddCore(this IServiceCollection services) {
            services.AddDbContext<DataContext>();
            return services;
        }
    }
}
