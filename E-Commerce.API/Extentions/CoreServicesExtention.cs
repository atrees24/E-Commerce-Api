using Service.Abstraction;
using Services;

namespace E_Commerce.API.Extentions
{
    public static class CoreServicesExtention
    {
        public static IServiceCollection AddCoreService(this IServiceCollection Services)
        {
            Services.AddScoped<IServiceManager, ServiceManager>();
            Services.AddAutoMapper(typeof(Services.AssempleReferance).Assembly);
            return Services;
        }

    }
}
