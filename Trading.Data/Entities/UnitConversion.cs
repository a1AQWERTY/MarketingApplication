using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Trading.Data.Entities
{
    [Table("UnitConversion")]

    public class UnitConversion
    {
        /// <summary>
        /// Identity Key of Unit Conversion
        /// </summary>
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column("UnitCoversionId")]
        public Guid UnitCoversionId { get; set; }

        /// <summary>
        /// Gets or sets Item Code
        /// </summary>
        [Column("FromUnitMasterId")]
        public Guid FromUnitMasterId { get; set; }

        /// <summary>
        /// Gets or sets Item Name
        /// </summary>
        [Column("ToUnitMasterId")]
        public Guid ToUnitMasterId { get; set; }

        /// <summary>
        /// Gets or sets Item Descritpion
        /// </summary>
        [Column("ConversionValue")]
        public decimal ConversionValue { get; set; }



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
