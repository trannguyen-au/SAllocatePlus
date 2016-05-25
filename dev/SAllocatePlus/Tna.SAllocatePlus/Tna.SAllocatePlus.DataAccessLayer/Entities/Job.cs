using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using Tna.SAllocatePlus.CommonShared;

namespace Tna.SAllocatePlus.DataAccessLayer.Entities
{
    public class Job
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int BookID { get; set; }
        [Required]
        public string SiteName { get; set; }
        [Required]
        public string SiteAddress { get; set; }

        [Required]
        public JobStageEnum JobStage { get; set; }
        public DateTime? JobDate { get; set; }
        public TimeSpan? JobTime { get; set; }
        public int StaffRequired { get; set; }
        public string JobDetails { get; set; }
        public virtual Staff JobSupervisor { get; set; }

        public virtual Region JobRegion { get; set; }
        public virtual List<JobStaff> JobStaffList { get; set; }
    }
}
