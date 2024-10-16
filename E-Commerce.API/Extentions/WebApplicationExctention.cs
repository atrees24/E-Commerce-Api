using Domain.Interfaces;
using E_Commerce.API.Middlewares;

namespace E_Commerce.API.Extentions
{
    public static class WebApplicationExctention
    {

        public  static async Task<WebApplication> SeedDBAsync(this WebApplication app)
        {
            using var scope = app.Services.CreateScope();
            var dbinitlizer = scope.ServiceProvider.GetRequiredService<IDBintilaizer>();
            await dbinitlizer.Initilaize();
            await dbinitlizer.InitilaizeIdentityAsync();

            return app;
        }


        public static WebApplication UseCustomeExceptionMiddelware(this WebApplication app)
        {
            app.UseMiddleware<GlobalErrorHandelingMiddelware>();
            return app;
        }
       
    }
}
