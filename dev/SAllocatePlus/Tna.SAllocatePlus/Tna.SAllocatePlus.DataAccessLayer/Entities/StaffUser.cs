using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Data;
using System.ComponentModel.DataAnnotations.Schema;

namespace Tna.SAllocatePlus.DataAccessLayer.Entities
{
    public class StaffUser
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        public Guid StaffUserID { get; set; }

        [Required]
        [MaxLength(100)]
        public string Username {get;set;}

        [Required]
        [MaxLength(100)]
        public string Password { get; set; }

        [MaxLength(100)]
        [Required]
        public string Email { get; set; }

        [Required]
        [MaxLength(100)]
        public string StaffName { get; set; }
    }
}
