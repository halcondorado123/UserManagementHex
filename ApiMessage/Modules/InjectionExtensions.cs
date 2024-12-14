using ApiMessage.Application.Interface;
using ApiMessage.Application.Main;
using ApiMessage.Data;
using ApiMessage.Domain.Core;
using ApiMessage.Infraestructure.Repository;
using ApiMessage.Transversal.Common;
using ApiMessage.Transversal.Logging;
using ApiResponse.Domain.Interface;
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
            services.AddScoped<IUserDomain, UserDomain>();
            services.AddScoped<IUserApplication, UserApplication>();
            services.AddScoped(typeof(IAppLogger<>), typeof(LoggerAdapter<>));


            return services;
        }
    }
}
