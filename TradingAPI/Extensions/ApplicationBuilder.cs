using Microsoft.AspNetCore.Builder;
using Swashbuckle.AspNetCore.SwaggerUI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TradingAPI.Extensions
{
    public static class ApplicationBuilder
    {
        public static IApplicationBuilder UseAppSwagger(this IApplicationBuilder _iApplicationBuilder)
        {
            _iApplicationBuilder.UseSwagger();

            _iApplicationBuilder.UseSwaggerUI(c =>
            {
                c.EnableFilter();
                c.EnableValidator();
                c.EnableDeepLinking();
                c.DisplayRequestDuration();
                c.DocExpansion(DocExpansion.List);
                c.DefaultModelRendering(ModelRendering.Example);
                c.SwaggerEndpoint("./v1/swagger.json", "Trading API V1");
                c.SwaggerEndpoint($"/swagger/v2.0/swagger.json", "Version 2.0");



            });

            return _iApplicationBuilder;
        }
    }
}
