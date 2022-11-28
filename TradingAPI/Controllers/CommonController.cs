using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Net;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Trading.Data.Common;
using Trading.Exception;
using TradingAPI.Extensions;
using TradingAPI.HandleRequest.Request.User;
using TradingAPI.HandleRequest.Response.Item;

namespace TradingAPI.Controllers
{
    [ApiVersion("1")]
    [Route("api/v{version:apiVersion}")]
    public class CommonController : Controller
    {
        private readonly AppSetting _appSetting;
        readonly IMediator _mediator;
        public CommonController(IOptions<AppSetting> appSetting, IMediator mediator)
        {
            _appSetting = appSetting.Value;
            _mediator = mediator;
        }

        [HttpPost]
        [Route("Login")]
        [ProducesResponseType(typeof(ResponseUserDetail), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(CustomException), (int)HttpStatusCode.InternalServerError)]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        [ProducesResponseType((int)HttpStatusCode.NoContent)]
        [ProducesResponseType((int)HttpStatusCode.Unauthorized)]

        //POST : /api/ApplicationUser/Login
        public async Task<IActionResult> Login(RequestUserToken request)
        {
            try
            {
                return Ok(_mediator.Send(request).Result);
            }
            catch (Exception ex)
            {

                throw;
            }
        }

        [HttpPost]
        [Route("ValidateTFA")]
        [ProducesResponseType(typeof(ResponseUserDetail), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(CustomException), (int)HttpStatusCode.InternalServerError)]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        [ProducesResponseType((int)HttpStatusCode.NoContent)]
        [ProducesResponseType((int)HttpStatusCode.Unauthorized)]

        //POST : /api/ApplicationUser/ValidateTFA
        public async Task<IActionResult> ValidateTFA([FromBody]RequestVerifyTFA request)
        {
            try
            {
                return Ok(_mediator.Send(request).Result);
            }
            catch (Exception)
            {

                throw;
            }
        }

    }
}
