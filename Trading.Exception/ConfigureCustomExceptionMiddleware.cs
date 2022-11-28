using Microsoft.AspNetCore.Builder;
using System;
using System.Collections.Generic;
using System.Text;

namespace Trading.Exception
{
    public static class ConfigureCustomExceptionMiddlewareRegister
    {
        public static void ConfigureCustomExceptionMiddleware(this IApplicationBuilder app)
        {
            app.UseMiddleware(typeof(ExceptionMiddleware));
        }
    }
}
