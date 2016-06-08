using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;

namespace Tna.SAllocatePlus.DataAccessLayer.Entities
{
    public class CostCentre
    {
        [Key, MaxLength(20)]
        public string CostCentreCode { get; set; }

        [MaxLength(50)]
        public string Name { get; set; }

        [MaxLength(150)]
        public string Email { get; set; }
    }
}
