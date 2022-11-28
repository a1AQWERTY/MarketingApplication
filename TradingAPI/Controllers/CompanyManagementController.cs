using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Trading.Exception;
using TradingAPI.Common;
using TradingAPI.Extensions;
using TradingAPI.HandleRequest.Request.Company;
using TradingAPI.HandleRequest.Request.ItemMaster;
using TradingAPI.HandleRequest.Request.Validations;
using TradingAPI.HandleRequest.Response;

namespace TradingAPI.Controllers
{
    [ApiVersion("1")]
    [Route("api/v{version:apiVersion}")]
    [Authorize]
    public class CompanyManagementController : BaseController
    {
        public readonly IMediator _mediator;
        #region Constructor
        public CompanyManagementController(IMediator mediator)
        {
            _mediator = mediator;
        }
        #endregion

        /// <summary>
        /// Add Company Data.
        /// </summary>
        // Post: api/Company
        [HttpPost]
        [ProducesResponseType(typeof(Guid), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(CustomException), (int)HttpStatusCode.InternalServerError)]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        [ProducesResponseType((int)HttpStatusCode.NoContent)]
        [ProducesResponseType((int)HttpStatusCode.Unauthorized)]
        [Route("Company")]
        public IActionResult AddCompany([FromBody] RequestAddUpdateCompany request)
        {
            SetIdentity(ref request);
            return Ok(_mediator.Send(request).Result);

        }
    }
}
