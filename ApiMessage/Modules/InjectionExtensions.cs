using ApiMessage.Data;
using System.Runtime.CompilerServices;

namespace ApiMessage.Modules
{
    public static class InjectionExtensions
    {
        public static IServiceCollection AddInjection(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddSingleton<IConfiguration>(configuration);
            services.AddSingleton<DapperContext>();
            services.AddScoped<IUserRepository, UserRepository>();

            return services;
        }
    }
}
