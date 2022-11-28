using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace TradingAPI.HandleRequest.Response.Item
{
    public class ResponseGetItemDetail
    {
        public string ItemCode { get; set; }

        public string ItemName { get; set; }


        public string ItemDescription { get; set; }

        [JsonPropertyName("ItemUnitId")]
        public Guid UnitMasterId { get; set; }

    }
}
