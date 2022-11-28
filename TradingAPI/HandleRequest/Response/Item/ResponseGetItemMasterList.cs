using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TradingAPI.HandleRequest.Response.Item
{
    public class ResponseGetItemMasterList
    {
        /// <summary>
        /// Gets or sets Item Master Unique Id
        /// </summary>
        public Guid ItemMasterId { get; set; }

        /// <summary>
        /// Gets or sets item code
        /// </summary>
        public string ItemCode { get; set; }

        /// <summary>
        /// Gets or sets Item Name
        /// </summary>
        public string ItemName { get; set; }

        /// <summary>
        /// Gets or set Item Description
        /// </summary>
        public string ItemDescription { get; set; }

        /// <summary>
        /// Gets or sets Unit Master Id
        /// </summary>
        public Guid UnitMasterId { get; set; }

        /// <summary>
        /// Gets or sets Unit Name
        /// </summary>
        public string UnitName { get; set; }
    }
}
