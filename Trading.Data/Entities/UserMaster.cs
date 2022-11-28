using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Trading.Data.Entities
{
    [Table("UserMaster")]
    public class UserMaster
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column("UserId")]
        public Guid UserId { get; set; }

        [Column("UserName")]
        public string UserName { get; set; }

        [Column("UserEmail")]
        public string UserEmail { get; set; }

        [Column("FirstName")]
        public string FirstName { get; set; }

        [Column("LastName")]
        public string LastName { get; set; }

        [Column("ContactNo")]
        public Int64 ContactNo { get; set; }

        [Column("CreatedBy")]
        public Guid? CreatedBy { get; set; }

        [Column("CreatedDate")]
        public DateTime? CreatedDate { get; set; }

        [Column("ModifiedBy")]
        public Guid? ModifiedBy { get; set; }

        [Column("ModifiedDate")]
        public DateTime? ModifiedDate { get; set; }

        [Column("IsDeleted")]
        [DefaultValue(0)]
        public bool IsDeleted { get; set; }

        [Column("UserUnId")]

        public string UserUnId { get; set; }
        [Column("Password")]
        public string Password { get; set; }
    }
}
