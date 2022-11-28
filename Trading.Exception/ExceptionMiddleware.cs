
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace Trading.Exception
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
          
            catch (CustomException ex)
            {
               
                await HandleExceptionAsync(httpContext, ex);
            }
            catch (System.Exception ex)
            {
               
                await HandleExceptionAsync(httpContext, ex);
            }

        }

        private Task HandleExceptionAsync(HttpContext context, System.Exception exception)
        {
            CustomExceptionModel MessageData = new CustomExceptionModel();

            string result;
            if (exception.InnerException.GetType() == typeof(CustomException))
            {
                var DataException = (CustomException)exception.InnerException;

                MessageData.Code = (int)DataException.statusCode;
                if (DataException.errros?.Any() == true)
                {
                    result = JsonConvert.SerializeObject(DataException.errros);
                }
                else
                {
                    MessageData.Message = DataException.Message;
                    result = JsonConvert.SerializeObject(MessageData);
                }
            }
            else
            {

                MessageData.Code = (int)HttpStatusCode.InternalServerError;
                MessageData.Message = exception.InnerException?.InnerException?.Message;
                result = JsonConvert.SerializeObject(MessageData);
            }

            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
            return context.Response.WriteAsync(result);
        }


    }

   
        
}
