using System;
using System.Net;
using System.Text.Json;
using System.Threading.Tasks;
using API.Errors;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace API.Middleware
{
    public class ExceptionMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<ExceptionMiddleware> _logger;
        private readonly IHostEnvironment _env;
        private const int ERROR_STATUS_CODE = (int)HttpStatusCode.InternalServerError;

        public ExceptionMiddleware(RequestDelegate next
            , ILogger<ExceptionMiddleware> logger
            , IHostEnvironment env)
        {
            _next = next;
            _logger = logger;
            _env = env;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                await SetupErrorResponse(context, ex);
            }
        }

        private async Task SetupErrorResponse(HttpContext context, Exception ex)
        {
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = ERROR_STATUS_CODE;
            await context.Response.WriteAsync(GetDetailErrorResponseByEnvironment(ex));
        }

        private string GetDetailErrorResponseByEnvironment(Exception ex)
        {
            var response = _env.IsDevelopment() ?
                new ApiException(ERROR_STATUS_CODE, ex.Message, ex.StackTrace)
                : new ApiException(ERROR_STATUS_CODE);

            return JsonSerializer.Serialize(response, new JsonSerializerOptions
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase
            });
        }
    }
}