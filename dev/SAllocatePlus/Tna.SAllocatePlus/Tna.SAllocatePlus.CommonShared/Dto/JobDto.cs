using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Tna.SAllocatePlus.CommonShared.Dto
{
    [DataContract]
    public class JobDto
    {
        [DataMember]
        public int BookID { get; set; }
        [DataMember]
        public string SiteName { get; set; }
        [DataMember]
        public string SiteAddress { get; set; }
        [DataMember]
        public JobStageEnum JobStage { get; set; }
        [DataMember]
        public DateTime? JobDate { get; set; }
        [DataMember]
        public TimeSpan? JobTime { get; set; }
        [DataMember]
        public int StaffRequired { get; set; }
        [DataMember]
        public string JobDetails { get; set; }
        [DataMember]
        public int SupervisorStaffID { get; set; }
        [DataMember]
        public string SupervisorName { get; set; }
        [DataMember]
        public string JobRegion { get; set; }
        [DataMember]
        public virtual List<JobStaffDto> JobStaffList { get; set; }
    }
}
