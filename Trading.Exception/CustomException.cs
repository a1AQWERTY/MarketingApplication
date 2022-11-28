
using FluentValidation.Results;
using System;
using System.Collections.Generic;
using System.Net;
using System.Security.Cryptography.X509Certificates;
using System.Text;

namespace Trading.Exception
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

       public List<CustomExceptionModel> errros;
        public CustomException(List<CustomExceptionModel> failures)
        { this.errros = failures; }

        public HttpStatusCode statusCode { get; set; }


    }

    public class CustomExceptionModel
    {
        public int Code { get; set; }
        public string Message { get; set; }
    }


}
