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
        /// <summary>
        /// Because MVC Controller is stateless, therefore to achieve a stateful BO 
        /// behaviour of the Business Logic Server, the handle or object referencing 
        /// of the BO object have to be maintained over the whole User session. In 
        /// order to achieve this, we use the Web Session to store the handle of BO
        /// for each user session in MVC Controller.
        /// </summary>
 
        ShopCartBOClient _shopCartService
        {
            get
            {
                if (Session["cartBoHandle"] == null)
                {
                    Session["cartBoHandle"] = new ShopCartBOClient("wsHttpBinding_IShopCartBO");
                }

                return (ShopCartBOClient)Session["cartBoHandle"];
            }
        }

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
        public ActionResult View()
        {
            var vm = new CartViewModel();
            vm.ProductList = _shopCartService.GetCart().ToList();
            vm.CartTotal = _shopCartService.GetTotalCart();

            if (Request.QueryString["UpdateSuccess"] != null)
            {
                vm.Message = "Cart updated successfully";
            }

            return View(vm);
        }

        [HttpPost]
        public ActionResult UpdateCart(List<ProductDto> item, string action)
        {
            if (item == null)
            {
                return RedirectToAction("View");
            }
            if (action == "Update Cart")
            {
                _shopCartService.ChangeQuantity(item.ToArray());

                return RedirectToAction("View", new { UpdateSuccess = true });
            }
            else if (action == "Checkout")
            {
                _shopCartService.CheckOut();
                return RedirectToAction("CheckoutSuccess");
            }
            return RedirectToAction("View");
        }

        public ActionResult CheckoutSuccess()
        {
            return View();
        }
    }
}
