using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Trading.Interface.Interface;
using TradingAPI.HandleRequest.Request.ItemMaster;
using System.Net;
using MediatR;

using Microsoft.AspNetCore.Authorization;
using TradingAPI.Common;
using Trading.Exception;
using TradingAPI.HandleRequest.Response.Item;
using Trading.Data.BusinessEntity.RequestFilter;
using Microsoft.AspNetCore.Http;
using TradingAPI.HandleRequest.Request.UploadMaster;
using TradingAPI.Attributes;
namespace TradingAPI.Controllers
{

    [ApiVersion("1")]
    [Route("api/v{version:apiVersion}")]
    [Authorize]
    public class ItemManageController : BaseController
    {
        public readonly IMediator _mediator;
        #region Constructor
        public ItemManageController(IMediator mediator)
        {
            _mediator = mediator;
        }



        /// <summary>
        /// Post Items.
        /// </summary>
        // Post: api/Items
        [HttpPost]
        [ProducesResponseType(typeof(Guid), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(List<CustomException>), (int)HttpStatusCode.InternalServerError)]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        [ProducesResponseType((int)HttpStatusCode.PreconditionFailed)]
        [ProducesResponseType((int)HttpStatusCode.NoContent)]
        [ProducesResponseType((int)HttpStatusCode.Unauthorized)]
        [Route("item")]
       
        public IActionResult AddItem([FromBody] RequestItemAdd request)
        {
            SetIdentity<RequestItemAdd>(ref request);
            return Ok(_mediator.Send(request).Result);
        }


        // Get: api/Item/itemId
        [HttpGet]
        [ProducesResponseType(typeof(ResponseGetItemDetail), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(List<CustomException>), (int)HttpStatusCode.InternalServerError)]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        [ProducesResponseType((int)HttpStatusCode.PreconditionFailed)]
        [ProducesResponseType((int)HttpStatusCode.NoContent)]
        [ProducesResponseType((int)HttpStatusCode.Unauthorized)]
        [Route("item/{itemMasterId}")]
        public IActionResult GetItemDetail(Guid itemMasterId)
        {
            RequestGetItemDetail request = new RequestGetItemDetail()
            {
                ItemMasterId = itemMasterId
            };
            SetIdentity(ref request);
            return Ok(_mediator.Send(request).Result);
        }

        /// <summary>
        /// Update Item.
        /// </summary>
        // Put: api/Items/{itemMasterId}
        [HttpPut]
        [ProducesResponseType(typeof(bool), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(List<CustomException>), (int)HttpStatusCode.InternalServerError)]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        [ProducesResponseType((int)HttpStatusCode.PreconditionFailed)]
        [ProducesResponseType((int)HttpStatusCode.NoContent)]
        [ProducesResponseType((int)HttpStatusCode.Unauthorized)]
        [Route("item/{itemMasterId}")]
        public IActionResult UpdateItem([FromBody] RequestItemUpdate request, Guid itemMasterId)
        {
            request.ItemMasterId = itemMasterId;
            SetIdentity(ref request);
            return Ok(_mediator.Send(request).Result);
        }


        /// <summary>
        /// Get Item Master list.
        /// </summary>
        // Get: api/Items
        [HttpGet]
        [ProducesResponseType(typeof(ResponseListClass<List<ResponseGetItemMasterList>>), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(List<CustomException>), (int)HttpStatusCode.InternalServerError)]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        [ProducesResponseType((int)HttpStatusCode.PreconditionFailed)]
        [ProducesResponseType((int)HttpStatusCode.NoContent)]
        [ProducesResponseType((int)HttpStatusCode.Unauthorized)]
        [Route("items")]
        [AllowAnonymous]
        public IActionResult GetItems(int PageNo = 0, int PageSize = 10)
        {

            RequestGetItemMasterList request = new RequestGetItemMasterList();
            request.requestFilter = new RequestFilter()
            {
                PageNo = PageNo,
                PageSize = PageSize
            };
            SetIdentity(ref request);
            return Ok(_mediator.Send(request).Result);
        }


        /// <summary>
        /// Get Item Inventory list.
        /// </summary>
        // Get: api/ItemInventory
        [HttpGet]
        [ProducesResponseType(typeof(ResponseListClass<List<ResponseGetItemInventoryList>>), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(List<CustomException>), (int)HttpStatusCode.InternalServerError)]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        [ProducesResponseType((int)HttpStatusCode.PreconditionFailed)]
        [ProducesResponseType((int)HttpStatusCode.NoContent)]
        [ProducesResponseType((int)HttpStatusCode.Unauthorized)]
        [Route("item/inventory")]
        public IActionResult GetItemInventory(int PageNo = 0, int PageSize = 10)
        {

            RequestGetItemInventoryList request = new RequestGetItemInventoryList();
            request.requestFilter = new RequestFilter()
            {
                PageNo = PageNo,
                PageSize = PageSize
            };
            SetIdentity(ref request);
            return Ok(_mediator.Send(request).Result);
        }

        /// <summary>
        /// Post Item Inventory.
        /// </summary>
        // Post: api/ItemInventory
        [HttpPost]
        [ProducesResponseType(typeof(bool), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(List<CustomException>), (int)HttpStatusCode.InternalServerError)]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        [ProducesResponseType((int)HttpStatusCode.PreconditionFailed)]
        [ProducesResponseType((int)HttpStatusCode.NoContent)]
        [ProducesResponseType((int)HttpStatusCode.Unauthorized)]
        [Route("item/inventory")]
        public IActionResult AddItemInventory([FromBody] RequestAddItemInventory request)
        {
            SetIdentity(ref request);
            return Ok(_mediator.Send(request).Result);
        }

        // Get: api/Item/Inventory/itemInventoryId
        [HttpGet]
        [ProducesResponseType(typeof(ResponseGetItemInventoryDetail), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(List<CustomException>), (int)HttpStatusCode.InternalServerError)]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        [ProducesResponseType((int)HttpStatusCode.PreconditionFailed)]
        [ProducesResponseType((int)HttpStatusCode.NoContent)]
        [ProducesResponseType((int)HttpStatusCode.Unauthorized)]
        [Route("item/inventory/{itemInventoryId}")]
        public IActionResult GetItemInventoryDetail(Guid itemInventoryId)
        {
            RequestGetItemInventoryDetail request = new RequestGetItemInventoryDetail()
            {
                ItemInventoryId = itemInventoryId
            };
            SetIdentity(ref request);
            return Ok(_mediator.Send(request).Result);
        }

        // Get: api/Item/Inventory/itemMasterId
        [HttpGet]
        [ProducesResponseType(typeof(ResponseListClass<List<ResponseGetItemWiseInventoryList>>), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(List<CustomException>), (int)HttpStatusCode.InternalServerError)]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        [ProducesResponseType((int)HttpStatusCode.PreconditionFailed)]
        [ProducesResponseType((int)HttpStatusCode.NoContent)]
        [ProducesResponseType((int)HttpStatusCode.Unauthorized)]
        [Route("item/inventory/{itemMasterId}/itemWise")]
        public IActionResult GetItemWiseInventoryDetail(Guid itemMasterId)
        {
            RequestGetItemWiseInventoryList request = new RequestGetItemWiseInventoryList()
            {
                ItemMasterId = itemMasterId
            };
            SetIdentity(ref request);
            return Ok(_mediator.Send(request).Result);
        }


        /// <summary>
        /// Put Item Inventory.
        /// </summary>
        // Post: api/ItemInventory
        [HttpPut]
        [ProducesResponseType(typeof(bool), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(List<CustomException>), (int)HttpStatusCode.InternalServerError)]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        [ProducesResponseType((int)HttpStatusCode.PreconditionFailed)]
        [ProducesResponseType((int)HttpStatusCode.NoContent)]
        [ProducesResponseType((int)HttpStatusCode.Unauthorized)]
        [Route("item/inventory/{itemInventoryId}")]
        public IActionResult UpdateItemInventory(Guid itemInventoryId, [FromBody] RequestUpdateItemInventory request)
        {
            request.ItemInventoryId = itemInventoryId;
            SetIdentity(ref request);
            return Ok(_mediator.Send(request).Result);
        }


        /// <summary>
        /// Post Item bom.
        /// </summary>
        // Post: api/Item BoM
        [HttpPost]
        [ProducesResponseType(typeof(bool), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(List<CustomException>), (int)HttpStatusCode.InternalServerError)]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        [ProducesResponseType((int)HttpStatusCode.PreconditionFailed)]
        [ProducesResponseType((int)HttpStatusCode.NoContent)]
        [ProducesResponseType((int)HttpStatusCode.Unauthorized)]
        [Route("item/bom")]
        public IActionResult AddItemBoM([FromBody] RequestAddItemBOM request)
        {
            SetIdentity(ref request);
            return Ok(_mediator.Send(request).Result);
        }

        /// <summary>
        /// Update Item BoM.
        /// </summary>
        // Put: api/Items/{ItemBoMMasterId}
        [HttpPut]
        [ProducesResponseType(typeof(bool), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(List<CustomException>), (int)HttpStatusCode.InternalServerError)]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        [ProducesResponseType((int)HttpStatusCode.PreconditionFailed)]
        [ProducesResponseType((int)HttpStatusCode.NoContent)]
        [ProducesResponseType((int)HttpStatusCode.Unauthorized)]
        [Route("item/bom/{ItemBoMMasterId}")]
        public IActionResult UpdateItem([FromBody] RequestUpdateItemBOM request, Guid ItemBoMMasterId)
        {
            request.ItemBomMasterId = ItemBoMMasterId;
            SetIdentity(ref request);
            return Ok(_mediator.Send(request).Result);
        }

        /// <summary>
        /// Delete Item BoM.
        /// </summary>
        // Delete: api/Items/{ItemBoMMasterId}
        [HttpDelete]
        [ProducesResponseType(typeof(bool), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(List<CustomException>), (int)HttpStatusCode.InternalServerError)]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        [ProducesResponseType((int)HttpStatusCode.PreconditionFailed)]
        [ProducesResponseType((int)HttpStatusCode.NoContent)]
        [ProducesResponseType((int)HttpStatusCode.Unauthorized)]
        [Route("item/bom/{ItemBoMId}/{isParent}")]
        public IActionResult UpdateItem(Guid ItemBoMId, bool isParent = true)
        {
            RequestDeleteItemBoM request = new RequestDeleteItemBoM()
            {
                ItemBomDeleteId = ItemBoMId,
                IsParent = isParent
            };
            SetIdentity(ref request);
            return Ok(_mediator.Send(request).Result);
        }


        /// <summary>
        /// Get Item BoM detail.
        /// </summary>
        // Get: api/Item/bom/itembomid
        [HttpGet]
        [ProducesResponseType(typeof(ResponseGetItemBoMDetail), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(List<CustomException>), (int)HttpStatusCode.InternalServerError)]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        [ProducesResponseType((int)HttpStatusCode.PreconditionFailed)]
        [ProducesResponseType((int)HttpStatusCode.NoContent)]
        [ProducesResponseType((int)HttpStatusCode.Unauthorized)]
        [Route("items/bom/{itembomid}")]
        public IActionResult GetItemBom(Guid itembomid)
        {
            RequestGetItemBoMDetail request = new RequestGetItemBoMDetail()
            {
                ItemBoMMasterId = itembomid
            };
            SetIdentity(ref request);
            return Ok(_mediator.Send(request).Result);
        }

        /// <summary>
        /// Get Item BoM list.
        /// </summary>
        // Get: api/ItemBom
        [HttpGet]
        [ProducesResponseType(typeof(ResponseListClass<List<ResponseGetItemBomList>>), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(List<CustomException>), (int)HttpStatusCode.InternalServerError)]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        [ProducesResponseType((int)HttpStatusCode.PreconditionFailed)]
        [ProducesResponseType((int)HttpStatusCode.NoContent)]
        [ProducesResponseType((int)HttpStatusCode.Unauthorized)]
        [Route("item/itemBom")]
        public IActionResult GetItemBom(int PageNo = 0, int PageSize = 10)
        {

            RequestGetItemBomList request = new RequestGetItemBomList();
            request.requestFilter = new RequestFilter()
            {
                PageNo = PageNo,
                PageSize = PageSize
            };
            SetIdentity(ref request);
            return Ok(_mediator.Send(request).Result);
        }
        #endregion

        // POST api/Attachments
        [HttpPost]
        [AllowAnonymous]
        [ProducesResponseType(typeof(ResponseListClass<List<ResponseGetItemBomList>>), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(List<CustomException>), (int)HttpStatusCode.InternalServerError)]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        [ProducesResponseType((int)HttpStatusCode.PreconditionFailed)]
        [ProducesResponseType((int)HttpStatusCode.NoContent)]
        [ProducesResponseType((int)HttpStatusCode.Unauthorized)]
        [Route("uploadMaster/{MasterType}")]
        public IActionResult PostFiles(string MasterType, IFormFile file)
        {


            RequestUploadMaster request = new RequestUploadMaster();
            request.file = file;
            request.MasterType = MasterType;
            SetIdentity(ref request);
            return Ok(_mediator.Send(request).Result);
           
        }
    }
}
