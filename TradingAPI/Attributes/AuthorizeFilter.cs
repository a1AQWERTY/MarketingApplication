using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TradingAPI.Attributes
{


    public class AuthorizeUserFilter : Attribute, IAuthorizationFilter
    {
        private string _userType { get; set; }
        private FontStyle _fontStyle { get; set; }
        public AuthorizeUserFilter(string userType, FontStyle fontStyle)
        {
            _userType = userType;
            _fontStyle = fontStyle;
        }

        public void OnAuthorization(AuthorizationFilterContext context)
        {
            //var hasClaim = context.HttpContext.User.Claims.Any(c => c.Type == _claim.Type && c.Value == _claim.Value);
            //if (!hasClaim)
            //{
            //    context.Result = new ForbidResult();
            //}
        }
    }
}

[Flags]
public enum FontStyle
{
    Bold = 1,
    Italic = 2,
    Regular = 0,
    Strikeout = 8,
    Underline = 4
}
