using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TradingAPI.HandleRequest.Response.Item
{
    public class ResponseGetItemBoMDetail
    {
        public Guid ItemMasterId { get; set; }
        public string ItemName { get; set; }

        public string ItemCode { get; set; }

        public decimal Quantity { get; set; }


        public Guid UnitMasterId { get; set; }

        public string UnitName { get; set; }

        public List<ResponseItemBoMChildDetail> ItemBoMChildDetails;
    }

    public class ResponseItemBoMChildDetail
    {

        public Guid ItemBoMChildId { get; set; }
        public Guid ItemMasterId { get; set; }
        public string ItemName { get; set; }

        public string ItemDescription { get; set; }

        public string ItemCode { get; set; }

        public decimal Quantity { get; set; }


        public Guid UnitMasterId { get; set; }

        public string UnitName { get; set; }
    }
}
