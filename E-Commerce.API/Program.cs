using Domain.Interfaces;
using E_Commerce.API.Extentions;
using E_Commerce.API.Factories;
using E_Commerce.API.Middlewares;
using Microsoft.AspNetCore.Mvc;
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
            #region Services
            // Add services to the container.
            builder.Services.AddCoreService();
            builder.Services.AddInfrastrcutreService(builder.Configuration);
            builder.Services.AddPresentationService();

            #endregion


            var app = builder.Build();

            #region Middelwares
            await app.SeedDBAsync();
            app.UseCustomeExceptionMiddelware();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseStaticFiles();
            app.UseHttpsRedirection();

            app.UseAuthorization();

            app.MapControllers();

            app.Run();

           
            #endregion
        }
    }
}
