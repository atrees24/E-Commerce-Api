using Service.Abstraction;
using Services;
using Shared;

namespace E_Commerce.API.Extentions
{
    public static class CoreServicesExtention
    {
        public static IServiceCollection AddCoreService(this IServiceCollection Services, IConfiguration configuration)
        {
            Services.AddScoped<IServiceManager, ServiceManager>();
            Services.AddAutoMapper(typeof(Services.AssempleReferance).Assembly);

            Services.Configure<JwtOptions>(configuration.GetSection("JwtOptions"));
            return Services;
        }

    }
}
