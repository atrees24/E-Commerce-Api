using Domain.Interfaces;
using Domain.Models;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Persistance;
using Persistance.Data;
using Persistance.Identity;
using Persistance.Repositories;
using Shared;
using StackExchange.Redis;
using System.Text;

namespace E_Commerce.API.Extentions
{
    public static class InfrastructreServicesExctention
    {
        public static IServiceCollection AddInfrastrcutreService(this IServiceCollection Services
            , IConfiguration configuration)
        {
            Services.AddScoped<IDBintilaizer, DBintilaizer>();
            Services.AddScoped<IUniteOfWork, UniteOfWork>();
            Services.AddScoped<IBasketRepository, BasketRepository>();
            Services.AddDbContext<StoreContext>(options =>
            {
                options.UseSqlServer(configuration.GetConnectionString("DefaultSQlConnection"));
            });
            Services.AddDbContext<StoreIdentityContext>(options =>
            {
                options.UseSqlServer(configuration.GetConnectionString("IdentitySQlConnection"));
            });
            Services.AddSingleton<IConnectionMultiplexer>(
                _ => ConnectionMultiplexer.Connect(
                    configuration.GetConnectionString("Redis")!));

            Services.ConfigureIdentityService();
            Services.ConfigureJwt(configuration);
            return Services;
        }



        public static IServiceCollection ConfigureIdentityService(this IServiceCollection Services)
        {
            Services.AddIdentity<User, IdentityRole>(options =>
            {
                options.Password.RequireDigit = true;
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequireUppercase = false;
                options.Password.RequireLowercase = false;
                options.Password.RequiredLength = 8;
                options.User.RequireUniqueEmail = true;
            }
            )
                .AddEntityFrameworkStores<StoreIdentityContext>();
            return Services;
        }


        public static IServiceCollection ConfigureJwt(this IServiceCollection services, IConfiguration configuration)
        {
            var jwtOptions = configuration.GetSection("JwtOptions").Get<JwtOptions>();

            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;

                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;

            }).AddJwtBearer(options =>

            {

                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,


                    ValidIssuer = jwtOptions.Issure,
                    ValidAudience = jwtOptions.Audience,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtOptions.SecretKey))

                };
            });

            services.AddAuthorization();

            return services;
        }
    }

}
