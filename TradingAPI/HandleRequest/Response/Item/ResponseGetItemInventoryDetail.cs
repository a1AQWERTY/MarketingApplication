using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TradingAPI.HandleRequest.Response.Item
{
    public class ResponseGetItemInventoryDetail
    {
        /// <summary>
        /// Gets or Sets Item Name
        /// </summary>
        public string ItemName { get; set; }

        /// <summary>
        /// Gets or Sets Item Master Id
        /// </summary>
        public Guid ItemMasterId { get; set; }

        /// <summary>
        /// Gets or Sets Unit Master Id
        /// </summary>
        public Guid UnitMasterId { get; set; }

        /// <summary>
        /// Gets or Sets Unit Name
        /// </summary>
        public string UnitName { get; set; }

        /// <summary>
        /// Gets or Sets Item Rate
        /// </summary>
        public decimal Rate { get; set; }

        /// <summary>
        /// Gets or Sets Item Quantity
        /// </summary>
        public decimal Quantity { get; set; }

        /// <summary>
        /// Gets or sets Item Batch No
        /// </summary>
        public string BatchNo { get; set; }
    }
}
