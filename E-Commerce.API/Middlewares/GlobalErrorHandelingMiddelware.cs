using Domain.Exceptions;
using Microsoft.AspNetCore.Http;
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
                if (httpContext.Response.StatusCode == (int)HttpStatusCode.NotFound)
                    await HandelNotFoundEndPointAsync(httpContext);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong {ex}");

                await HandelExceptionAsync(httpContext, ex);
            }

           
        }

        private async Task HandelNotFoundEndPointAsync(HttpContext httpContext)
        {
            httpContext.Response.ContentType = "application/json";
            var response = new ErrorDetails
                {
                  StatusCode = (int)HttpStatusCode.NotFound,
                  ErrorMessage = $"The EndPoint {httpContext.Request.Path} is not responding."
            }.ToString();

            await httpContext.Response.WriteAsync(response);
        }

        private async Task HandelExceptionAsync(HttpContext httpContext, Exception ex)
        {
            httpContext.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
            httpContext.Response.ContentType = "application/json";

            var response = new ErrorDetails
            {
                
                ErrorMessage = ex.Message
            };

            httpContext.Response.StatusCode = ex switch
            {
                NotFoundException => (int)HttpStatusCode.NotFound,
                UnAthuraizedException => (int)HttpStatusCode.Unauthorized,
                ValidationException validationException => HandelValidationExceptionAsync(validationException,response),
                _ => (int)HttpStatusCode.InternalServerError
            };


            response.StatusCode = httpContext.Response.StatusCode;

            await httpContext.Response.WriteAsync(response.ToString());
        }

        private int HandelValidationExceptionAsync(ValidationException validationException, ErrorDetails response)
        {
            response.Errors= validationException.Errors;
            return (int)HttpStatusCode.BadRequest;
        }
    }
}
