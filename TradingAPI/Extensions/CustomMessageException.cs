using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace TradingAPI.Extensions
{
    public class CustomMessageException : System.Exception
    {
        private string _exceptionMessage = string.Empty;

        public string ExceptionMessage { get { return _exceptionMessage; } set { _exceptionMessage = value; } }

        public CustomMessageException() : base() { }

        public CustomMessageException(string exceptionMessage) : base(exceptionMessage)
        {
            _exceptionMessage = exceptionMessage;
        }

        public CustomMessageException(string exceptionMessage, string message) : base(message)
        {
            _exceptionMessage = exceptionMessage;
        }

        public CustomMessageException(string exceptionMessage, string message, System.Exception innerException) : base(message, innerException)
        {
            _exceptionMessage = exceptionMessage;
        }
    }

    public class CustomException : System.Exception
    {
        public CustomException()
        {

        }

        public CustomException(string message)
            : base(message)
        { }

        public CustomException(string message, System.Exception innerException)
            : base(message, innerException)
        { }

        public CustomException(List<CustomExceptionModel> failures)
        { }

        public HttpStatusCode statusCode { get; set; }


    }

    [Serializable]
    public class CustomExceptionModel
    {
        public int Code { get; set; }
        public string Message { get; set; }
    }
}
