using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tna.SAllocatePlus.CommonShared.Dto
{
    public class SendEmailRequestDto
    {
        public List<int> JobList { get; set; }
        public List<int> StaffList { get; set; }
        public string Content { get; set; }
    }
}
