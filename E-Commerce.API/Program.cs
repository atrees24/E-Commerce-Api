using Domain.Interfaces;
using Microsoft.EntityFrameworkCore;
using Persistance;
using Persistance.Data;
using Persistance.Repositories;
using Service.Abstraction;
using Services;
using System.Collections.Concurrent;

namespace E_Commerce.API
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddControllers().AddApplicationPart(typeof(Presentation.AssimbleByReferance).Assembly);
            builder.Services.AddScoped<IDBintilaizer, DBintilaizer>();
            builder.Services.AddScoped<IUniteOfWork, UniteOfWork>();
            builder.Services.AddScoped<IServiceManager, ServiceManager>();
            builder.Services.AddAutoMapper(typeof(Services.AssempleReferance).Assembly);
            builder.Services.AddDbContext<StoreContext>(options =>
            {
                options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultSQlConnection"));
            });

            // Register ConcurrentDictionary<string, object>
            builder.Services.AddSingleton(new ConcurrentDictionary<string, object>());

            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            var app = builder.Build();
            await InitilaizeDB(app);

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();

            app.MapControllers();

            app.Run();

            async Task InitilaizeDB(WebApplication app)
            {
                using var scope = app.Services.CreateScope();
                var dbinitlizer = scope.ServiceProvider.GetRequiredService<IDBintilaizer>();
                await dbinitlizer.Initilaize();
            }
        }
    }
}
