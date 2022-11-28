using MediatR;
using System;
using System.Collections.Generic;
using Trading.Data.Common;

namespace TradingAPI.HandleRequest.Request.ItemMaster
{
    public class RequestAddItemBOM : RequestBase, IRequest<Guid>
    {
        public Guid ItemMasterId { get; set; }

        public Guid UnitMasterId { get; set; }

        public decimal Quantity { get; set; }

        public List<RequestItemBoMChild> itemBoMChild { get; set; }
    }

    public class RequestItemBoMChild
    {
        public Guid ItemMasterId { get; set; }

        public Guid UnitMasterId { get; set; }

        public decimal Quantity { get; set; }
    }
}
