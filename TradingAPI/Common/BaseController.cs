using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace TradingAPI.Common
{
    public class BaseController : Controller
    {
        public void SetIdentity<T>(ref T request)
        {
            object userEmailValue = HttpContext.User.Claims.FirstOrDefault(k => k.Type == "UserName")?.Value;
            if (userEmailValue != null)
            {
                SetPropertyValue(request, "requestUserEmail", userEmailValue);
            }

            object userIdValue = HttpContext.User.Claims.FirstOrDefault(k => k.Type == "UserId")?.Value;
            if (userIdValue != null)
            {
                SetPropertyValue(request, "requestUserId", userIdValue);
            }
        }

        public void SetPropertyValue(object requestObject, string PropertyName, object PropertyValue)
        {
            PropertyInfo propertyInfo = requestObject.GetType().GetProperty(PropertyName);
            if (propertyInfo != null)
            {
                object finalValue = null;
                Type t = propertyInfo.PropertyType;

                if (t == typeof(string))
                {
                    finalValue = Convert.ChangeType(PropertyValue, t, CultureInfo.InvariantCulture);
                }
                else if (t == typeof(Guid))
                {
                    finalValue = Convert.ChangeType(Guid.Parse(PropertyValue.ToString()), typeof(Guid), CultureInfo.InvariantCulture);
                }
                else if (t == typeof(int))
                {
                    finalValue = Convert.ChangeType(Convert.ToInt64(PropertyValue.ToString()), typeof(int), CultureInfo.InvariantCulture);
                }

                propertyInfo.SetValue(requestObject, finalValue, null);
            }
        }


    }
}
