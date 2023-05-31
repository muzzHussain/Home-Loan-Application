using Microsoft.AspNetCore.Builder;

namespace Utility.Global_Exceptions
{
    public static class ExceptionExtensions
    {
        /// <summary>
        ///     Adding Global Exception into MiddleWare
        /// </summary>
        /// <param name="app"></param>
        public static void ConfigureExceptionHandler(this IApplicationBuilder app)
        {
            app.UseMiddleware<GlobalException>();
        }
    }
}
