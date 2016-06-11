using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tna.SAllocatePlus.CommonShared.Dto
{
    public class JobAvailabilityDto
    {
        public int BookID { get; set; }
        public int StaffID { get; set; }
        public string StaffName { get; set; }
        public bool? IsAvailable { get; set; }
    }
}
