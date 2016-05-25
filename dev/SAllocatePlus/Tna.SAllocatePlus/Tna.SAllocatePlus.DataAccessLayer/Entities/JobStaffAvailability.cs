using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;

namespace Tna.SAllocatePlus.DataAccessLayer.Entities
{
    public class JobStaffAvailability
    {
        [Key, Column(Order = 1), ForeignKey("Job")]
        public int BookID { get; set; }

        [Key, Column(Order = 2), ForeignKey("Staff")]
        public int StaffID { get; set; }

        public Job Job { get; set; }

        public Staff Staff { get; set; }
        
        public bool IsAvailable { get; set; }
    }
}
