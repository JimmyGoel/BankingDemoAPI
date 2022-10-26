using ApplicationCore.Exceptions;
using ApplicationCore.Extensions;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace PublicApi.MiddleWare
{
    public class ExecptionMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<ExecptionMiddleware> _logger;
        private readonly IHostEnvironment _host;
        public ExecptionMiddleware(RequestDelegate next,
            ILogger<ExecptionMiddleware> logger,IHostEnvironment host )
        {
            this._next = next;
            this._logger = logger;
            this._host = host;
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
                await HandleExceptionAsync(context, ex);
            }
        }

        private async Task HandleExceptionAsync(HttpContext context, Exception ex)
        {
            context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
            context.Response.ContentType = "application/json";

            var response = _host.IsDevelopment()
                ? new ApiError(context.Response.StatusCode, ex.Message, ex.StackTrace?.ToString())
                : new ApiError(context.Response.StatusCode, "Internal Server error");

            await context.Response.WriteAsync(response.ToJson());
        }
    }
}
