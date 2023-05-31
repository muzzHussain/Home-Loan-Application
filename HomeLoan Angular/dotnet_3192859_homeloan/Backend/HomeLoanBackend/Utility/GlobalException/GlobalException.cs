using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Utility.Global_Exceptions
{
    internal class GlobalException
    {
        private readonly RequestDelegate _next;
        private ILogger _logger;

        public GlobalException(RequestDelegate next, ILogger logger)
        {
            _logger = logger;
            _next = next;
        }

        /// <summary>
        ///     To fetch next http context if possible. Otherwise throw exception.
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (ArgumentException exception)
            {
                _logger.LogError(exception);
                await HandleExceptionAsync(context, 400, exception.Message);                
            }
            catch(AccessViolationException exception)
            {
                _logger.LogError(exception);
                await HandleExceptionAsync(context, 403, exception.Message);
            }
            catch (Exception exception)
            {
                _logger.LogError(exception);
                await HandleExceptionAsync(context, 500, "Internal server error");
            }
        }

        public async Task HandleExceptionAsync(HttpContext context, int statusCode, string message)
        {
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = statusCode;
            await context.Response.WriteAsync(message);
        }
    }
}
