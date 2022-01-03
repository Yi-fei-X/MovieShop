using ApplicationCore.Exceptions;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace MovieShopMVC.Infrastructure
{
    // You may need to install the Microsoft.AspNetCore.Http.Abstractions package into your project
    public class MovieShopExceptionMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<MovieShopExceptionMiddleware> _logger;
        public MovieShopExceptionMiddleware(RequestDelegate next, ILogger<MovieShopExceptionMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task Invoke(HttpContext httpContext)
        {
            _logger.LogInformation("Exception Middleware Begining");
            try
            {
                // when everything is good
                await _next(httpContext);
            }
            catch (Exception ex)
            {
                // when any exception happens, handle it here
                _logger.LogError($"Catching the exception in Middleware{ex}");
                await HandleException(httpContext, ex);     // The method that handle the exception.
            }
        }

        private async Task HandleException(HttpContext httpContext, Exception ex)
        {
            // catch the actual type of exception, set the http status code.
            // log exceptions details using SeriLog to either Text or JSON file
            // send email using MailKit to developer team
            // display a friendly page to User

            switch (ex)
            {
                case ConflictException _:   //underscore is to check the type
                    httpContext.Response.StatusCode = (int)HttpStatusCode.BadRequest;
                    break;
                case NotFoundException _:
                    httpContext.Response.StatusCode = (int)HttpStatusCode.NotFound;
                    break;
                default:
                    httpContext.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                    break;
            }

            httpContext.Response.Redirect("/Home/Error");
            await Task.CompletedTask;

        }
    }

    // Extension method used to add the middleware to the HTTP request pipeline.
    public static class MovieShopExceptionMiddlewareExtensions
    {
        public static IApplicationBuilder UseMovieShopExceptionMiddleware(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<MovieShopExceptionMiddleware>();
        }
    }
}
