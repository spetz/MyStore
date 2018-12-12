using System;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace MyStore.Web.Framework
{
    public class ErrorHandlerMiddleware : IMiddleware
    {
        private readonly ILogger<ErrorHandlerMiddleware> _logger;

        public ErrorHandlerMiddleware(ILogger<ErrorHandlerMiddleware> logger)
        {
            _logger = logger;
        }

        public async Task InvokeAsync(HttpContext context, RequestDelegate next)
        {
            try
            {
                await next(context);
            }
            catch (Exception e)
            {
                _logger.LogError(e, e.Message);
                await HandleErrorAsync(context, e);
            }
        }

        private async Task HandleErrorAsync(HttpContext context, Exception exception)
        {
            var errorCode = "error";
            var statusCode = HttpStatusCode.BadRequest;
            var message = exception.Message;
            switch (exception)
            {
                case ArgumentException a:
                    errorCode = "argument";
                    break;
            }

            var response = new
            {
                code = errorCode,
                message
            };
            var payload = JsonConvert.SerializeObject(response);
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int) statusCode;

            await context.Response.WriteAsync(payload);
        }
    }
}