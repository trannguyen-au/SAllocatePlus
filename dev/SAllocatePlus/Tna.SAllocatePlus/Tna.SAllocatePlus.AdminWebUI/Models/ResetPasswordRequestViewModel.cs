using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Tna.SAllocatePlus.AdminWebUI.Models
{
    public class ResetPasswordRequestViewModel
    {
        [Required]
        public int StaffID { get; set; }
        public string Name { get; set; }
        [Required]
        public string OldPassword { get; set; }
        [Required]
        public string NewPassword { get; set; }
        public string ConfirmPassword { get; set; }
    }
}