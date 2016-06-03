using Newtonsoft.Json;
using SwinSchool.CommonShared;
using SwinSchool.WebUI.Models;
using SwinSchool.WebUI.Security;
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

        [AllowAnonymous]
        public ActionResult Login()
        {
            if(User.Identity.IsAuthenticated)
            {
                if (User.IsInRole(Constants.RoleValue.Administrator))
                {
                    return RedirectToAction("Index", "Admin");
                }
                else if (User.IsInRole(Constants.RoleValue.Employee))
                {
                    return RedirectToAction("Index", "Employee");
                }
                else
                {
                    return RedirectToAction("Index", "Home");
                }
            }
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        public ActionResult Login(LoginViewModel vm, string returnUrl = "")
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var userPrincipal = UserPrincipal.ValidateLogin(vm.UserName, vm.Password);
                    var userData = JsonConvert.SerializeObject(userPrincipal.SerializedData);

                    FormsAuthenticationTicket authTicket = new FormsAuthenticationTicket(
                        1,
                        userPrincipal.Identity.Name,
                        DateTime.Now,
                        DateTime.Now.AddMinutes(30),
                        false, //pass here true, if you want to implement remember me functionality
                        userData);

                    string encTicket = FormsAuthentication.Encrypt(authTicket);
                    HttpCookie faCookie = new HttpCookie(FormsAuthentication.FormsCookieName, encTicket);
                    Response.Cookies.Add(faCookie);

                    if(returnUrl!="")
                    {
                        return Redirect(returnUrl);
                    }
                    else if (userPrincipal.IsInRole(Constants.RoleValue.Administrator))
                    {
                        return RedirectToAction("Index", "Admin");
                    }
                    else if (userPrincipal.IsInRole(Constants.RoleValue.Employee))
                    {
                        return RedirectToAction("Index", "Employee");
                    }
                    else
                    {
                        return RedirectToAction("Index", "Home");
                    }
                }
                catch (Exception)
                {
                    ModelState.AddModelError("", "Incorrect username and/or password");
                }
            }

            return View(vm);
        }

        [AllowAnonymous]
        public ActionResult LogOut()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("Login", "Account", null);
        }
    }
}
