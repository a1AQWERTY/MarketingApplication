using MediatR;
using System;
using System.Collections.Generic;
using Trading.Data.Common;
using TradingAPI.HandleRequest.Response.Item;

namespace TradingAPI.HandleRequest.Request.ItemMaster
{
    public class RequestGetItemInventoryDetail : RequestBase, IRequest<ResponseGetItemInventoryDetail>
    {
        public Guid ItemInventoryId { get; set; }
    }
}
