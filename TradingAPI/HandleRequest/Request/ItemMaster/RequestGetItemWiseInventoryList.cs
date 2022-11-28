using System;
using System.Collections.Generic;
using MediatR;
using Trading.Data.Common;
using TradingAPI.Common;
using TradingAPI.HandleRequest.Response.Item;

namespace TradingAPI.HandleRequest.Request.ItemMaster
{
    public class RequestGetItemWiseInventoryList : RequestBase, IRequest<ResponseListClass<List<ResponseGetItemWiseInventoryList>>>
    {
        public Guid ItemMasterId { get; set; }
    }
}
