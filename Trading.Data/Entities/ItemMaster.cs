using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Trading.Data.Entities
{
    [Table("ItemMaster")]
    public class ItemMaster
    {

        /// <summary>
        /// Identity Key of Item Master
        /// </summary>
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column("ItemMasterId")]
        public Guid ItemMasterId { get; set; }


        /// <summary>
        /// Gets or sets Item Code
        /// </summary>
        [Column("ItemCode")]
        public string ItemCode { get; set; }

        /// <summary>
        /// Gets or sets Item Name
        /// </summary>
        [Column("ItemName")]
        public string ItemName { get; set; }

        /// <summary>
        /// Gets or sets Item Descritpion
        /// </summary>
        [Column("ItemDescription")]
        public string ItemDescription { get; set; }

        /// <summary>
        /// Gets or sets unit
        /// </summary>
        [Column("UnitMasterId")]
        public Guid UnitMasterId { get; set; }

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
    }
}
