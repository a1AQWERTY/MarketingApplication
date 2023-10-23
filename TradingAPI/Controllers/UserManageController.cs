using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Net;
using Trading.Data.BusinessEntity.RequestFilter;
using Trading.Exception;
using TradingAPI.Common;
using TradingAPI.HandleRequest.Request.User;
using TradingAPI.HandleRequest.Response.Users;

namespace TradingAPI.Controllers
{
    [ApiVersion("1")]
    [Route("api/v{version:apiVersion}")]
    [AllowAnonymous]
    public class UserManageController : BaseController
    {
        public readonly IMediator _mediator;
        #region Constructor
        public UserManageController(IMediator mediator)
        {
            _mediator = mediator;
        }
        #endregion

       
       

        /// <summary>
        /// Add User.
        /// </summary>
        // GET: api/User
        [HttpPost]
        [ProducesResponseType(200)]
        [ProducesResponseType(204)]
        [ProducesResponseType(500)]
        [Route("User")]
        public IActionResult AddUser([FromBody]RequestAddUpdateUser request)
        {
            SetIdentity(ref request);

            return Ok(_mediator.Send(request).Result);
        }


        /// <summary>
        /// Get Item Master list.
        /// </summary>
        // Get: api/Items
        [HttpGet]
        [ProducesResponseType(typeof(ResponseListClass<List<ResponseGetUserList>>), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(List<CustomException>), (int)HttpStatusCode.InternalServerError)]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        [ProducesResponseType((int)HttpStatusCode.PreconditionFailed)]
        [ProducesResponseType((int)HttpStatusCode.NoContent)]
        [ProducesResponseType((int)HttpStatusCode.Unauthorized)]
        [Route("Users")]
        public IActionResult GetUsers(int PageNo = 0, int PageSize = 10)
        {

            RequestUsers request = new RequestUsers();
            request.requestFilter = new RequestFilter()
            {
                PageNo = PageNo,
                PageSize = PageSize
            };
            SetIdentity(ref request);
            return Ok(_mediator.Send(request).Result);
        }

    }
}

