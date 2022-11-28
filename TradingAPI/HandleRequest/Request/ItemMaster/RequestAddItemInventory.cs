using MediatR;
using System;
using Trading.Data.Common;

namespace TradingAPI.HandleRequest.Request.ItemMaster
{
    public class RequestAddItemInventory: RequestBase, IRequest<bool>
    {
        public Guid ItemMasterId { get; set; }

        public Guid UnitMasterId { get; set; }

        public decimal Quantity { get; set; }

        public decimal Rate { get; set; }

        public string BatchNo { get; set; }

    }
}
