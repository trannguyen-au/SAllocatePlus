using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;

namespace Tna.SAllocatePlus.DataAccessLayer.Entities
{
    public class Region
    {
        [Key, MaxLength(20)]
        public string RegionID { get; set; }

        public string RegionName { get; set; }
    }
}
