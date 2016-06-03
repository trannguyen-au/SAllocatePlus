using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace SwinSchool.WebUI.Models
{
    public class ResetPasswordRequestViewModel
    {
        [Required]
        public string UserID { get; set; }
        public string Name { get; set; }
        public string OldPassword { get; set; }
        public string NewPassword { get; set; }
        public string ConfirmPassword { get; set; }

    }
}