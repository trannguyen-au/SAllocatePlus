using SwinSchool.CommonShared.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SwinSchool.WebUI.Models
{
    public class ProductListViewModel
    {
        public List<ProductDto> ProductList { get; set; }
        public decimal CartTotal { get; set; }
        public int TotalCartQty { get; set; }
    }
}