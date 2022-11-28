using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Formatters;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;
using Newtonsoft.Json.Serialization;
using Swashbuckle.AspNetCore.SwaggerGen;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

using TradingAPI.Fliter;
using TradingAPI.HandleRequest.Request.Validations;

namespace TradingAPI.Extensions
{
    public static class ServiceCollections
    {
        public static IServiceCollection AddAppMVC(this IServiceCollection _iServiceCollection)
        {
            _iServiceCollection.AddControllers()
                  .AddNewtonsoftJson(options =>
                  {
                      options.SerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();

                  }).AddMvcOptions(options =>
                  {
                      options.OutputFormatters.Add(new XmlDataContractSerializerOutputFormatter());
                  });

            _iServiceCollection.AddMvc(config =>
            {
                //config.Filters.Add(typeof(CustomExceptionFilter));
                //config.Filters.Add(typeof(ValidatorActionFilter));
            }).AddFluentValidation(fv =>
            {
                fv.RegisterValidatorsFromAssembly(Assembly.GetExecutingAssembly());
            });
            _iServiceCollection.Configure<FormOptions>(x =>
            {
                x.MultipartBodyLengthLimit = 209715200;
            }); 



            return _iServiceCollection;
        }

        public static IServiceCollection AddAppSwaggerGen(this IServiceCollection _iServiceCollection)
        {
            _iServiceCollection.AddSwaggerGen(c =>
            {

                c.SwaggerDoc("v1", new OpenApiInfo { Title = "Trading Api", Version = "v1" });

                c.ResolveConflictingActions(apiDescriptions => apiDescriptions.First()); 


                c.OperationFilter<RemoveVersionFromParameter>();
                c.DocumentFilter<ReplaceVersionWithValue>();


                c.DocInclusionPredicate((docName, apiDesc) =>
                {
                    if (!apiDesc.TryGetMethodInfo(out MethodInfo methodInfo)) return false;

                    var versions = methodInfo.DeclaringType
                        .GetCustomAttributes(true)
                        .OfType<ApiVersionAttribute>()
                        .SelectMany(attr => attr.Versions);

                    return versions.Any(v => $"v{v.ToString()}" == docName);
                });

                c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme()
                {
                    Description = "JWT Authorization header using the Bearer scheme. Example: \"Authorization: Bearer {token}\"",
                    Name = "Authorization",
                    In = ParameterLocation.Header,
                    Type = SecuritySchemeType.ApiKey,
                    Scheme = "Bearer"
                });

                var security = new OpenApiSecurityRequirement()
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference
                            {
                                Type = ReferenceType.SecurityScheme,
                                Id = "Bearer"
                            },
                            Scheme = "oauth2",
                            Name = "Bearer",
                            In = ParameterLocation.Header,
                        },
                        new List<string>()

                    }
                };
                c.AddSecurityRequirement(security);

                var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                c.IncludeXmlComments(xmlPath);
            });

            return _iServiceCollection;
        }
    }
}
