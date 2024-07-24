using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Net;
using System.Threading.Tasks;
using System;
using datacapture.Modal;



namespace datacapture.Middleware
{
    public class CustomExceptionMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<CustomExceptionMiddleware> _logger;
        public CustomExceptionMiddleware(RequestDelegate next, ILogger<CustomExceptionMiddleware> logger)
        {
            _logger = logger;
            _next = next;
        }
        public async Task InvokeAsync(HttpContext httpContext)
        {
            try
            {
                await _next(httpContext);
            }
            catch (Exception ex)
            {
               

                await HandleExceptionAsync(httpContext, ex);
            }
        }
        private async Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int)GetStatusCode(exception);
            var apiError = GetErrorResponse(exception);
            await context.Response.WriteAsync(apiError.ToString());
        }

        private static HttpStatusCode GetStatusCode(Exception ex)
        {
            var exceptionType = ex.GetType();

            var exceptionStatusCodeDictionary = new Dictionary<Type, HttpStatusCode>()
            {
                {
                    typeof(BadHttpRequestException), HttpStatusCode.BadGateway
                },
                {
                    typeof(TimeoutException), HttpStatusCode.GatewayTimeout
                },
                {
                    typeof(InvalidDataException), HttpStatusCode.BadRequest
                },
                {
                    typeof(DuplicateNameException), HttpStatusCode.BadRequest
                }
            };

            return exceptionStatusCodeDictionary.GetValueOrDefault(exceptionType, HttpStatusCode.InternalServerError);
        }

        private static datacapture.Modal.ErrorResponse GetErrorResponse(Exception ex)
        {
            var apiError = new ErrorResponse
            {
                StatusCode = (int)GetStatusCode(ex),
                StatusPhrase = ex.GetType().Name,
                Timestamp = DateTime.Now
            };
            apiError.Errors.Add(ex.Message);

            return apiError;
        }
    }
}
