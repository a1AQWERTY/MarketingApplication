using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TradingAPI.HandleRequest.Response.Item
{
    public class ResponseGetItemWiseInventoryList
    {
        /// <summary>
        /// Gets or sets Item Code
        /// </summary>
        public string ItemCode { get; set; }

        /// <summary>
        /// Gets or sets Item Name
        /// </summary>
        public string ItemName { get; set; }

        /// <summary>
        /// Gets or sets Item Description
        /// </summary>
        public string ItemDescription { get; set; }

        /// <summary>
        /// Gets or sets Item Rate
        /// </summary>
        public string Rate { get; set; }

        /// <summary>
        /// Gets or sets Item Batch No
        /// </summary>
        public string BatchNo { get; set; }
    }
}
