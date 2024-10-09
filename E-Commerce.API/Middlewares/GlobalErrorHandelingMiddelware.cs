using Domain.Exceptions;
using Shared.ErrorModels;
using System.Net;

namespace E_Commerce.API.Middlewares
{
    public class GlobalErrorHandelingMiddelware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<GlobalErrorHandelingMiddelware> _logger;


        public GlobalErrorHandelingMiddelware(RequestDelegate next, ILogger<GlobalErrorHandelingMiddelware> logger)
        {
            _next = next;
            _logger = logger;
        }

       public async Task InvokeAsync(HttpContext httpContext)
        {
            try
            {
                await _next(httpContext);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong {ex}");

                await HandelExceptionAsync(httpContext, ex);
            }

           
        }

        private async Task HandelExceptionAsync(HttpContext httpContext, Exception ex)
        {
            httpContext.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
            httpContext.Response.ContentType = "application/json";

            httpContext.Response.StatusCode = ex switch
            {
                NotFoundException => (int)HttpStatusCode.NotFound,
                _ => (int)HttpStatusCode.InternalServerError
            };

           var response = new ErrorDetails
            {
                StatusCode = (int)httpContext.Response.StatusCode,
                ErrorMessage = ex.Message
            }.ToString();


            await httpContext.Response.WriteAsync(response);
        }



    }
}
