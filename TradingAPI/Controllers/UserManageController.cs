using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TradingAPI.Common;
using TradingAPI.HandleRequest.Request.User;

namespace TradingAPI.Controllers
{
    [ApiVersion("1")]
    [Route("api/v{version:apiVersion}")]
    [Authorize]
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
        /// Get Users.
        /// </summary>
        // GET: api/Users
        [HttpGet]
        [ProducesResponseType(200)]
        [ProducesResponseType(204)]
        [ProducesResponseType(500)]
        [Route("Users")]
        public IActionResult GetUsers()
        {

            return NoContent();
        }

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
    }
}

