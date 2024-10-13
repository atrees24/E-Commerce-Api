using Domain.Interfaces;
using Microsoft.EntityFrameworkCore;
using Persistance.Data;
using Persistance.Repositories;
using Persistance;
using System.Reflection;
using E_Commerce.API.Factories;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Concurrent;

namespace E_Commerce.API.Extentions
{
    public static class PresentationServicesExtention
    {

        public static IServiceCollection AddPresentationService(this IServiceCollection Services)
        {
            Services.AddControllers().AddApplicationPart(typeof(Presentation.AssimbleByReferance).Assembly);
            Services.AddSingleton(new ConcurrentDictionary<string, object>());
            Services.AddEndpointsApiExplorer();
            Services.AddSwaggerGen();
            Services.Configure<ApiBehaviorOptions>(options =>
            {
                options.InvalidModelStateResponseFactory = ApiResponseFactory.CustomeValidationErrorResponse;
            });
            return Services;
        }
        
    }
}
