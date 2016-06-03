using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SwinSchool.CommonShared.Dto
{
    public class ResetPasswordRequestDto
    {
        public string UserId { get; set; }
        public string OldPassword { get; set; }
        public string NewPassword { get; set; }
    }
}
