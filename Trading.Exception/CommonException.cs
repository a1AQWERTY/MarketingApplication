using System;
using System.Collections.Generic;
using System.Net;
using System.Text;

namespace Trading.Exception
{
    public static class CommonException
    {
        public static void NoDataFound(string PropertyName)
        {
            throw new CustomException(new List<CustomExceptionModel> { new CustomExceptionModel() {
                Message = "No Data found for " + PropertyName,
                Code = (int)HttpStatusCode.NotFound
            }
         });
        }

        public static void GeneralException(string Message)
        {
            throw new CustomException(new List<CustomExceptionModel> { new CustomExceptionModel() {
                Message = Message,
                Code = (int)HttpStatusCode.InternalServerError
            }
         });
        }
    }
}
