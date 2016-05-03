using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;

namespace SwinSchool.CommonShared.Dto
{
    [Serializable]
    [DataContract]
    public class ProductDto
    {
        [DataMember]
        public string ProductID { get; set; }
        [DataMember]
        public string Name { get; set; }
        [DataMember]
        public int OrderQuantity { get; set; }
        [DataMember]
        public decimal Price { get; set; }
        public decimal SubTotal
        {
            get
            {
                return OrderQuantity * Price;
            }
        }
    }
}
