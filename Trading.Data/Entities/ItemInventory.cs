using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.CompilerServices;
using System.Text;

namespace Trading.Data.Entities
{
    [Table("ItemInventory")]
    public class ItemInventory
    {
        /// <summary>
        /// Identity Key of Item Inventory
        /// </summary>
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column("ItemInventoryId")]
        public Guid ItemInventoryId { get; set; }


        /// <summary>
        /// Unique Id of Item Master
        /// </summary>
       
        [Column("ItemMasterId")]
        public Guid ItemMasterId { get; set; }

        /// <summary>
        /// Unique Id of Unit Master
        /// </summary>

        [Column("UnitMasterId")]
        public Guid UnitMasterId { get; set; }

        /// <summary>
        /// Quantity of item inventory
        /// </summary>

        [Column("Quantity")]
        public decimal Quantity { get; set; }

        /// <summary>
        /// Rate of item
        /// </summary>

        [Column("Rate")]
        public decimal Rate { get; set; }

        /// <summary>
        /// Created By
        /// </summary>
        [Column("CreatedBy")]
        public Guid CreatedBy { get; set; }


        /// <summary>
        /// Created on date
        /// </summary>
        [Column("CreatedDate")]
        public DateTime CreatedDate { get; set; }


        /// <summary>
        /// set true if deleted or else set false
        /// </summary>
        [Column("IsDeleted")]
        [DefaultValue(0)]
        public bool IsDeleted { get; set; }


        /// <summary>
        /// Modified By
        /// </summary>
        [Column("ModifiedBy")]
        public Guid? ModifiedBy { get; set; }

        /// <summary>
        /// Modified on date
        /// </summary>
        [Column("ModifiedDate")]
        public DateTime? ModifiedDate { get; set; }

        /// <summary>
        /// Batch Number
        /// </summary>
        [Column("BatchNo")]
        public string BatchNo { get; set; }
    }
}
