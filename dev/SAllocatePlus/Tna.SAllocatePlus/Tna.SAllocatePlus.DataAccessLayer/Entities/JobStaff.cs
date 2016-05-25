using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;

namespace Tna.SAllocatePlus.DataAccessLayer.Entities
{
    public class JobStaff
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int JobStaffID { get; set; }
        [Required]
        public virtual Staff Staff { get; set; }
        [Required]
        public virtual Job Job { get; set; }

        public TimeSpan? StartTime { get; set; }

        public DateTime? StartDate { get; set; }

        public bool IsSupervisor { get; set; }
        public bool IsStaffConfirmed { get; set; }
    }
}
