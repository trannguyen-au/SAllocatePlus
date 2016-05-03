using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;

namespace SwinSchool.DAL.Entities
{
    public class Product
    {
        [Key]
        [MaxLength(6)]
        public string ProductID { get; set; }
        
        [MaxLength(30)]
        public string Name { get; set; }
        public int Quantity { get; set; }
        public decimal Cost { get; set; }
        public decimal SellingPrice { get; set; }
    }
}
