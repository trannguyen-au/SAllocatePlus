using SwinSchool.CommonShared;
using SwinSchool.WebUI.Models;
using SwinSchool.WebUI.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace SwinSchool.WebUI.Controllers
{
    public class AccountController : Controller
    {
        MyUserBOClient _myUserService = new MyUserBOClient("wsHttpBinding_IMyUserBO");

        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(LoginViewModel vm)
        {
            var user = _myUserService.ValidateLogin(vm.UserName, vm.Password);
            if (user != null)
            {
                // persist user to cookie authentication ticket so that the subsequence requests will be authenticated.
                FormsAuthentication.SetAuthCookie(user.UserID, true);

                // authorization process
                if (user.Role == Constants.RoleValue.Administrator)
                {
                    return RedirectToAction("AdminUser");
                }
                else return RedirectToAction("UserProfile");
            }

            ModelState.AddModelError("LoginFailed", "Login failed");
            return View(vm);
        }

        [Authorize(Roles="Administrator")]
        public ActionResult AdminUser()
        {

            return View();
        }

        [Authorize(Roles = "Employee,Administrator")]
        public ActionResult UserProfile()
        {
            return View();
        }
    }
}
