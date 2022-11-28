using System;
using System.Collections.Generic;
using System.Text;

namespace Trading.Data.BusinessEntity
{
   public class ItemInventoryList
    {

        /// <summary>
        /// Gets or sets Item Inventory Unique Id
        /// </summary>
        public Guid ItemInventoryId { get; set; }

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

        /// <summary>
        /// Gets or sets Rate
        /// </summary>
        public decimal Rate { get; set; }

        /// <summary>
        /// Gets or sets Item Quantity
        /// </summary>
        public decimal Quantity { get; set; }

        /// <summary>
        /// Gets or sets Item Batch No
        /// </summary>
        public string BatchNo { get; set; }
    }
}
