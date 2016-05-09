using SwinSchool.CommonShared.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace SwinSchool.BusinessLogicServer
{
	// NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "IShopCartBO" in both code and config file together.
	[ServiceContract]
    
	public interface IShopCartBO
	{
		[OperationContract]
		void AddProduct(ProductDto product);
        [OperationContract]
        void ChangeQuantity(ProductDto[] updatedCart);
        [OperationContract]
        void RemoveProduct(ProductDto product);
        [OperationContract]
        List<ProductDto> GetCart();
        [OperationContract]
        List<ProductDto> GetAvailableProducts();
        [OperationContract]
        decimal GetTotalCart();
        [OperationContract]
        void CheckOut();
	}
}
