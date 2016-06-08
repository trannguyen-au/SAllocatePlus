using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tna.SAllocatePlus.CommonShared;

namespace Tna.SAllocatePlus.DataAccessLayer.Entities
{
    public class StaffAccessRight
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int AccessRightID { get; set; }
        
        [ForeignKey("StaffUser")]
        public int StaffUserID { get; set; }

        [ForeignKey("CostCentre")]
        public string CostCentreCode { get; set; }

        public AccessRightsEnum AccessRights { get; set; }

        [Required]
        public virtual CostCentre CostCentre { get; set; }
        [Required]
        public virtual Staff StaffUser { get; set; }
    }
}
