using MediatR;
using System.Collections.Generic;
using Trading.Data.BusinessEntity.RequestFilter;
using Trading.Data.Common;
using TradingAPI.Common;

using TradingAPI.HandleRequest.Response.Item;

namespace TradingAPI.HandleRequest.Request.ItemMaster
{
    public class RequestGetItemMasterList : RequestBase, IRequest<ResponseListClass<List<ResponseGetItemMasterList>>>
    {
        public RequestFilter requestFilter { get; set; }
    }
}
