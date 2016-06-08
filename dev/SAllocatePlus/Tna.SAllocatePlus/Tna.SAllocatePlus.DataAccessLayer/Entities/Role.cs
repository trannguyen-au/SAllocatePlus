using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Data;
using System.ComponentModel.DataAnnotations.Schema;

namespace Tna.SAllocatePlus.DataAccessLayer.Entities
{
    public class Role
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int RoleID { get; set; }

        [Required]
        [MaxLength(100)]
        public string RoleName {get;set;}

        public virtual List<Staff> StaffList { get; set; }
    }
}
