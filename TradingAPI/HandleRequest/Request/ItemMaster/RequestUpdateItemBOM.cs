using MediatR;
using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;
using Trading.Data.Common;

namespace TradingAPI.HandleRequest.Request.ItemMaster
{
    public class RequestUpdateItemBOM : RequestBase, IRequest<bool>
    {
        [JsonIgnore]
        public Guid ItemBomMasterId { get; set; }

        public Guid ItemMasterId { get; set; }

        public Guid UnitMasterId { get; set; }

        public decimal Quantity { get; set; }

        public List<RequestUpdateItemBoMChild> itemBoMChild { get; set; }
    }

    public class RequestUpdateItemBoMChild
    {
        public Guid? ItemBomChildId { get; set; }

        public Guid ItemMasterId { get; set; }

        public Guid UnitMasterId { get; set; }

        public decimal Quantity { get; set; }
    }
}
