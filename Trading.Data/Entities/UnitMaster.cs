using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace Trading.Data.Entities
{
    [Table("UnitMaster")]
    public class UnitMaster
    {
        /// <summary>
        /// Identity Key of Unit Master
        /// </summary>
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column("UnitMasterId")]
        public Guid UnitMasterId { get; set; }

        /// <summary>
        /// Gets or Set Unit Code
        /// </summary>
        [Column("UnitCode")]
        public string UnitCode { get; set; }

        /// <summary>
        /// Gets or sets Unit Name
        /// </summary>
        [Column("UnitName")]
        public string UnitName { get; set; }

        /// <summary>
        /// Gets or sets Unit Description
        /// </summary>
        [Column("UnitDescription")]
        public string UnitDescription { get; set; }


        /// <summary>
        /// Created By
        /// </summary>
        [Column("CreatedBy")]
        public Guid? CreatedBy { get; set; }

        /// <summary>
        /// Created on Date
        /// </summary>
        [Column("CreatedDate")]
        public DateTime? CreatedDate { get; set; }

      
        /// <summary>
        /// set true if deleted or else set false
        /// </summary>
        [Column("IsDeleted")]
        [DefaultValue(0)]
        public bool IsDeleted { get; set; }
    }
}
