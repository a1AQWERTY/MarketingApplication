using MediatR;
using System.Collections.Generic;
using Trading.Data.BusinessEntity.RequestFilter;
using Trading.Data.Common;
using TradingAPI.Common;

using TradingAPI.HandleRequest.Response.Item;

namespace TradingAPI.HandleRequest.Request.ItemMaster
{
    public class RequestGetItemInventoryList : RequestBase, IRequest<ResponseListClass<List<ResponseGetItemInventoryList>>>
    {
        public RequestFilter requestFilter { get; set; }
    }
}
