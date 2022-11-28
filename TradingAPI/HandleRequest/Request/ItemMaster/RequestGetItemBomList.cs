using MediatR;
using System.Collections.Generic;
using Trading.Data.BusinessEntity.RequestFilter;
using Trading.Data.Common;
using TradingAPI.Common;

using TradingAPI.HandleRequest.Response.Item;
namespace TradingAPI.HandleRequest.Request.ItemMaster
{
    public class RequestGetItemBomList : RequestBase, IRequest<ResponseListClass<List<ResponseGetItemBomList>>>
    {
        public RequestFilter requestFilter { get; set; }
    }
}
