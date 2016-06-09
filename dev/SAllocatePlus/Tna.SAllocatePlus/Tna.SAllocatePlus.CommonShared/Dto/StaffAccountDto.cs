using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tna.SAllocatePlus.CommonShared.Dto
{
    public class StaffAccountDto
    {
        public int StaffID { get;set;}
        public string FirstName { get; set; }
        public string SurName { get; set; }
        public string Mobile { get; set; }
        public string Email{ get; set; }
        public string Username{ get; set; }
        public bool Active{ get; set; }
        public string StaffCostCentre{ get; set; }
        public List<string> RoleList { get; set; }
        public List<AccessRightsDto> AccessList { get; set; }
    }
}
