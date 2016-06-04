using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;

namespace SwinSchool.DAL.Entities
{
    /// <summary>
    /// CodeFirst EF: MyUser model
    /// </summary>
    public class MyUser
    {
        [Key]
        [MaxLength(10)]
        public string UserID { get; set; }

        [MaxLength(30)]
        public string Name { get; set; }

        [MaxLength(12)]
        public string Password { get; set; }

        [MaxLength(100)]
        public string Email { get; set; }

        [MaxLength(10)]
        public string Tel { get; set; }

        [MaxLength(60)]
        public string Address { get; set; }

        [MaxLength(60)]
        public string SecQn { get; set; }

        [MaxLength(60)]
        public string SecAns { get; set; }
    }
}
