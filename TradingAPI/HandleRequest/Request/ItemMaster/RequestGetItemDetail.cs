using MediatR;
using System;
using System.Collections.Generic;
using Trading.Data.Common;
using TradingAPI.HandleRequest.Response.Item;

namespace TradingAPI.HandleRequest.Request.ItemMaster
{
    public class RequestGetItemDetail : RequestBase, IRequest<ResponseGetItemDetail>
    {
        public Guid ItemMasterId { get; set; }
    }
}
