using SwinSchool.CommonShared.Dto;
using SwinSchool.DAL.DAO;
using SwinSchool.DAL.DAO.Impl;
using SwinSchool.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace SwinSchool.BusinessLogicServer
{
    [ServiceBehavior(InstanceContextMode = InstanceContextMode.PerSession)]
    public class ShopCartBO : IShopCartBO
    {
        IProductDao _productDao;

        List<ProductDto> _cart { get; set; }
        public ShopCartBO()
        {
            _cart = new List<ProductDto>();
            _productDao = new ProductDao();
            
        }
        public void AddProduct(CommonShared.Dto.ProductDto product)
        {
            // find the product from db;
            var productEntity = _productDao.GetById(product.ProductID);
            if(productEntity==null)
                throw new Exception("Product is not found");

            // if adding same product, add to quantity
            var existingProduct = _cart.FirstOrDefault(p => p.ProductID == product.ProductID);
            if (existingProduct != null)
            {
                existingProduct.OrderQuantity += product.OrderQuantity;
            }
            else
            {
                _cart.Add(new ProductDto() {
                    ProductID = product.ProductID,
                    OrderQuantity = product.OrderQuantity,
                    Name = productEntity.Name,
                    Price = productEntity.SellingPrice
                });
            }
        }

        public void ChangeQuantity(CommonShared.Dto.ProductDto product, int quantity)
        {
            var productInCart = _cart.FirstOrDefault(p => p.ProductID == product.ProductID);
            if (productInCart == null)
                throw new Exception("Product is not exist in cart");

            if (quantity <= 0)
                throw new Exception("Quantity cannot be less than 1");

            productInCart.OrderQuantity = quantity;
        }

        public void RemoveProduct(CommonShared.Dto.ProductDto product)
        {
            var productToRemove = _cart.FirstOrDefault(p => p.ProductID == product.ProductID);
            if (productToRemove != null)
            {
                _cart.Remove(productToRemove);
            }
        }

        public List<CommonShared.Dto.ProductDto> GetCart()
        {
            ProductDto[] productList = new ProductDto[_cart.Count];
            _cart.CopyTo(productList,0);
            return productList.ToList();
        }

        public decimal GetTotalCart()
        {
            var total = 0m;
            _cart.ForEach(new Action<ProductDto>((p) => total+= p.SubTotal));
            return total;
        }


        public List<ProductDto> GetAvailableProducts()
        {
            return (from p in _productDao.GetAll()
                    select new ProductDto()
                    {
                        ProductID = p.ProductID,
                        Price = p.SellingPrice,
                        OrderQuantity = 0,
                        Name = p.Name
                    }).ToList();
        }


        public void CheckOut()
        {
            // save cart to database as an Order entity
            // then clear the cart
            _cart.Clear();
        }
    }
}
