using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Tna.SAllocatePlus.CommonShared.Dto
{
    [DataContract]
    public class JobStaffDto
    {
        [DataMember]
        public int JobStaffID { get; set; }
        [DataMember]
        public int BookID { get; set; }
        [DataMember]
        public int StaffID { get; set; }
        [DataMember]
        public TimeSpan? StartTime { get; set; }
        [DataMember]
        public DateTime? StartDate { get; set; }
        [DataMember]
        public bool IsSupervisor { get; set; }
        [DataMember]
        public bool IsStaffConfirmed { get; set; }
    }
}
