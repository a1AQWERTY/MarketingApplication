using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Trading.Data.Entities
{
    public class BoMItemDetails
    {
        public string ItemCode { get; set; }

        public string ItemDescription { get; set; }

        public string ItemName { get; set; }

        public string UnitName { get; set; }


    }
}
