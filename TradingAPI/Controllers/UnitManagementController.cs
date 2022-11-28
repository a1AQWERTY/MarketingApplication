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
using TradingAPI.HandleRequest.Request.Unit;
using TradingAPI.HandleRequest.Request.Validations;
using TradingAPI.HandleRequest.Response;
using TradingAPI.HandleRequest.Response.Unit;

namespace TradingAPI.Controllers
{
    [ApiVersion("1")]
    [Route("api/v{version:apiVersion}")]
    [Authorize]
    public class UnitManagementController : BaseController
    {
        public readonly IMediator _mediator;
        #region Constructor
        public UnitManagementController(IMediator mediator)
        {
            _mediator = mediator;
        }
        #endregion

        /// <summary>
        /// Get Unit Master Data.
        /// </summary>
        // Get: api/unit
        [HttpGet]
        [ProducesResponseType(typeof(ResponseListClass<List<ResponseGetUnitMaster>>), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(CustomException), (int)HttpStatusCode.InternalServerError)]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        [ProducesResponseType((int)HttpStatusCode.NoContent)]
        [ProducesResponseType((int)HttpStatusCode.Unauthorized)]
        [Route("unit")]
        public IActionResult getUnitMaster()
        {
            RequestGetUnitMaster request = new RequestGetUnitMaster();
            SetIdentity(ref request);
            return Ok(_mediator.Send(request).Result);

        }

        /// <summary>
        /// Post Unit Conversion.
        /// </summary>
        // Post: api/unitconversion
        [HttpPost]
        [ProducesResponseType(typeof(bool), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(List<CustomException>), (int)HttpStatusCode.InternalServerError)]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        [ProducesResponseType((int)HttpStatusCode.PreconditionFailed)]
        [ProducesResponseType((int)HttpStatusCode.NoContent)]
        [ProducesResponseType((int)HttpStatusCode.Unauthorized)]
        [Route("unitconversion")]
        public IActionResult AddItem([FromBody] RequestAddUnitConversion request)
        {
            SetIdentity<RequestAddUnitConversion>(ref request);
            return Ok(_mediator.Send(request).Result);
        }
    }
}
