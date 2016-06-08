using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;

namespace Tna.SAllocatePlus.DataAccessLayer.Entities
{
    public class Staff
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int StaffID { get; set; }

        [MaxLength(100)]
        [Required]
        public string FirstName { get; set; }

        [MaxLength(100)]
        public string SurName { get; set; }

        [MaxLength(30)]
        public string Mobile { get; set; }

        [Required]
        [MaxLength(100)]
        public string Email { get; set; }

        [MaxLength(100)]
        [Required]
        public string Username { get; set; }

        [MaxLength(100)]
        [Required]
        public string Password { get; set; }

        [DefaultValue(true)]
        public bool Active { get; set; }

        [ForeignKey("CostCentre")]
        public string StaffCostCentre { get; set; }

        [Required]
        public virtual CostCentre CostCentre { get; set; }

        public virtual List<Role> RoleList { get; set; }

        public virtual List<StaffAccessRight> AccessList { get; set; }
    }
}
