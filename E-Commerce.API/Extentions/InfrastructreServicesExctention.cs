using Domain.Interfaces;
using E_Commerce.API.Factories;
using Microsoft.AspNetCore.Mvc;
using Persistance.Data;
using Persistance.Repositories;
using Persistance;
using Service.Abstraction;
using Services;
using Microsoft.EntityFrameworkCore;
using StackExchange.Redis;

namespace E_Commerce.API.Extentions
{
    public static class InfrastructreServicesExctention
    {
        public static IServiceCollection AddInfrastrcutreService(this IServiceCollection Services , IConfiguration configuration)
        {
            Services.AddScoped<IDBintilaizer, DBintilaizer>();
            Services.AddScoped<IUniteOfWork, UniteOfWork>();
            Services.AddScoped<IBasketRepository, BasketRepository>();
            Services.AddDbContext<StoreContext>(options =>
            {
                options.UseSqlServer(configuration.GetConnectionString("DefaultSQlConnection"));
            });
            Services.AddSingleton<IConnectionMultiplexer>(
                _ => ConnectionMultiplexer.Connect(configuration.GetConnectionString("Redis")!));
            return Services;
        }
    }
}
