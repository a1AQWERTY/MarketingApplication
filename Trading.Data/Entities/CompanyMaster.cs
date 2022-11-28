using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Trading.Data.Entities
{
    [Table("CompanyMaster")]
    public class CompanyMaster
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column("CompanyMasterId")]
        public Guid CompanyMasterId { get; set; }

        [Column("Name")]
        public string Name { get; set; }

        [Column("Address")]
        public string Address { get; set; }

        [Column("ContactNo")]
        public Int64 ContactNo { get; set; }

        [Column("ContactPerson")]
        public string ContactPerson { get; set; }

        [Column("EmailId")]
        public string EmailId { get; set; }


        [Column("CreatedBy")]
        public Guid CreatedBy { get; set; }

        [Column("CreatedDate")]
        public DateTime CreatedDate { get; set; }

        [Column("IsDeleted")]
        [DefaultValue(0)]
        public bool IsDeleted { get; set; }

        [Column("ModifiedBy")]
        public Guid? ModifiedBy { get; set; }

        [Column("ModifiedDate")]
        public DateTime? ModifiedDate { get; set; }
    }
}
