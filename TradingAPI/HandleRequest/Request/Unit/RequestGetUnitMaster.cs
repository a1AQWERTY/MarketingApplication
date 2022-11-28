using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using Trading.Data.Common;
using TradingAPI.Common;
using TradingAPI.HandleRequest.Response.Unit;

namespace TradingAPI.HandleRequest.Request.Unit
{
    public class RequestGetUnitMaster : RequestBase, IRequest<ResponseListClass<List<ResponseGetUnitMaster>>>
    {
        
    }
}
