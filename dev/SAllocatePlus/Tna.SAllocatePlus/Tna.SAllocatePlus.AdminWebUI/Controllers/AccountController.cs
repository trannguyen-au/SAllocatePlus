using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using Tna.SAllocatePlus.AdminWebUI.Models;
using Tna.SAllocatePlus.AdminWebUI.Security;
using Tna.SAllocatePlus.CommonShared;

namespace Tna.SAllocatePlus.AdminWebUI.Controllers
{
    public class AccountController : Controller
    {
        [AllowAnonymous]
        public ActionResult Login()
        {
            if (User.Identity.IsAuthenticated)
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

                    if (returnUrl != "")
                    {
                        return Redirect(returnUrl);
                    }
                    else if (userPrincipal.IsInRole(Constants.RoleValue.Administrator))
                    {
                        return RedirectToAction("Index", "Job");
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

        [Authorize]
        public ActionResult LogOut()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("Login", "Account", null);
        }
    }
}