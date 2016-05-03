using SwinSchool.CommonShared.Dto;
using SwinSchool.WebUI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SwinSchool.WebUI.Controllers
{
    public class CartController : Controller
    {

        static ShopCartBOClient _shopCartService = new ShopCartBOClient("wsHttpBinding_IShopCartBO");

        //
        // GET: /Cart/
        public ActionResult Index()
        {
            var vm = new ProductListViewModel();
            vm.ProductList = _shopCartService.GetAvailableProducts().ToList();
            vm.CartTotal = _shopCartService.GetTotalCart();
            vm.TotalCartQty = _shopCartService.GetCart().Length;
            return View(vm);
        }

        [HttpGet]
        public ActionResult Add(string id)
        {
            ProductDto dto = new ProductDto()
            {
                ProductID = id,
                OrderQuantity = 1
            };
            _shopCartService.AddProduct(dto);
            var a = _shopCartService.GetTotalCart();
            return RedirectToAction("Index");
        }
    }
}
