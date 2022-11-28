
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System.Globalization;
using System.Net;
using System.Threading.Tasks;
namespace TradingAPI.Extensions
{
    public class ExceptionMiddleware
    {
        private readonly RequestDelegate _next;
        // private readonly ILogger _logger;
        private readonly JsonSerializerSettings _jsonSettings;

        public ExceptionMiddleware(RequestDelegate next)
        {
            //_logger = logger;
            _next = next;

            _jsonSettings = new JsonSerializerSettings()
            {
                ReferenceLoopHandling = ReferenceLoopHandling.Ignore,
                Formatting = Formatting.Indented,
                ContractResolver = new CamelCasePropertyNamesContractResolver(),
                NullValueHandling = NullValueHandling.Ignore
            };
        }

        public async Task InvokeAsync(HttpContext httpContext)
        {
            try
            {
                await _next(httpContext);
            }
            catch (CustomException cm)
            {
                if (httpContext.Response.HasStarted)
                {
                    throw;
                }

                httpContext.Response.StatusCode = 500;
                httpContext.Response.ContentType = "application/json";
                httpContext.Response.Headers.Add("exception", "messageException");
                var json = JsonConvert.SerializeObject(new { Message = cm.Message }, _jsonSettings);
                await httpContext.Response.WriteAsync(json);
            }

            catch (System.Exception ex)
            {
                //_logger.LogError($"Something went wrong: {ex}");
                await HandleExceptionAsync(httpContext, ex);
            }

        }

        private Task HandleExceptionAsync(HttpContext context, System.Exception exception)
        {


            string result = string.Empty;
            CustomExceptionModel MessageData = new CustomExceptionModel();

            if (exception.InnerException.GetType() == typeof(CustomException))
            {
                var DataException = (CustomException)exception.InnerException;

                MessageData.Code = (int)DataException.statusCode;
                MessageData.Message = DataException.Message;

                result = JsonConvert.SerializeObject(MessageData);
            }
            else
            {

                MessageData.Code = (int)HttpStatusCode.InternalServerError;
                MessageData.Message = exception.Message;
                result = JsonConvert.SerializeObject(MessageData);
            }

            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
            return context.Response.WriteAsync(result);

        }


    }
}
