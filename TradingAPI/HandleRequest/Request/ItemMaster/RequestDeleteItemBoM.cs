using MediatR;
using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;
using Trading.Data.Common;

namespace TradingAPI.HandleRequest.Request.ItemMaster
{
    public class RequestDeleteItemBoM : RequestBase, IRequest<bool>
    {
        public Guid ItemBomDeleteId { get; set; }

        public bool IsParent { get; set; }
    }
}
