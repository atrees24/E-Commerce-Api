using Domain.Interfaces;
using Microsoft.EntityFrameworkCore;
using Persistance.Data;
using Persistance.Repositories;
using Persistance;
using System.Reflection;
using E_Commerce.API.Factories;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Concurrent;
using Microsoft.OpenApi.Models;

namespace E_Commerce.API.Extentions
{
    public static class PresentationServicesExtention
    {

        public static IServiceCollection AddPresentationService(this IServiceCollection Services)
        {
            Services.AddControllers().AddApplicationPart(typeof(Presentation.AssimbleByReferance).Assembly);
            Services.AddSingleton(new ConcurrentDictionary<string, object>());
            Services.ConfigureSwagger();
            Services.Configure<ApiBehaviorOptions>(options =>
            {
                options.InvalidModelStateResponseFactory = ApiResponseFactory.CustomeValidationErrorResponse;
            });
            return Services;
        }


        public static IServiceCollection ConfigureSwagger(this IServiceCollection Services)
        {
            Services.AddEndpointsApiExplorer();
            Services.AddSwaggerGen( options =>
            {
                options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    In = ParameterLocation.Header,
                    Description = "Plaese enter Baerer Token!",
                    Name = "Authoriztion",
                    Type = SecuritySchemeType.Http,
                    Scheme = "Bearer",
                    BearerFormat = "JWT"
                });

                options.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference
                            {
                                Type = ReferenceType.SecurityScheme,
                                Id ="Bearer"
                            }
                        },
                        new List<string>(){ }

                    }
                });
            });
            return Services;
        }
    }
}
