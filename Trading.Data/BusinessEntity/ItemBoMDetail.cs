using System;
using System.Collections.Generic;
using System.Text;

namespace Trading.Data.BusinessEntity
{
    public class ItemBoMDetail
    {
        public Guid ItemMasterId { get; set; }
        public string ItemName { get; set; }

        public string ItemCode { get; set; }

        public decimal Quantity { get; set; }


        public Guid UnitMasterId { get; set; }

        public string UnitName { get; set; }

        public List<ItemBoMChildDetail> ItemBoMChildDetails;
    }

    public class ItemBoMChildDetail
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
