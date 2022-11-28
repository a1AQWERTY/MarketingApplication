using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TradingAPI.Fliter
{
    public class ReplaceVersionWithValue : IDocumentFilter
    {
        public void Apply(OpenApiDocument swaggerDoc, DocumentFilterContext context)
        {
            var paths = swaggerDoc.Paths.Select(path => new { Key = path.Key.Replace("v{version}", swaggerDoc.Info.Version), path.Value }).ToList();

            swaggerDoc.Paths = new OpenApiPaths();
            foreach (var it in paths)
            {
                swaggerDoc.Paths.Add(it.Key, it.Value);
            }
        }
    }
}
