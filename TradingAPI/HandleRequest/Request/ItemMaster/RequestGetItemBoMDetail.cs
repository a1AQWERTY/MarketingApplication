using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Trading.Data.Common;
using TradingAPI.HandleRequest.Response.Item;

namespace TradingAPI.HandleRequest.Request.ItemMaster
{
    public class RequestGetItemBoMDetail : RequestBase, IRequest<ResponseGetItemBoMDetail>
    {
        /// <summary>
        /// sets item bom master id
        /// </summary>
        public Guid ItemBoMMasterId { get; set; }
    }
}
