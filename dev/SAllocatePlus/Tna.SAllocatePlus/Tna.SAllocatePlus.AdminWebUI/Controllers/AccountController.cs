using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using Tna.SAllocatePlus.AdminWebUI.Models;
using Tna.SAllocatePlus.AdminWebUI.Security;
using Tna.SAllocatePlus.ClientServices;
using Tna.SAllocatePlus.CommonShared;
using Tna.SAllocatePlus.CommonShared.Dto;

namespace Tna.SAllocatePlus.AdminWebUI.Controllers
{
    [AllowAnonymous]
    public class AccountController : Controller
    {
        AccountServiceClient _accountClient;
        CommonDataServiceClient _commonDataService;

        public AccountController()
        {
            _accountClient = ServiceFactory.CreateAccountServiceClient();
            _commonDataService = ServiceFactory.CreateCommonDataServiceClient();
        }

        public ActionResult Login()
        {
            if (User.Identity.IsAuthenticated)
            {
                if (User.IsInRole(Constants.RoleValue.Administrator))
                {
                    return RedirectToAction("Index", "Job");
                }
                else if (User.IsInRole(Constants.RoleValue.Employee))
                {
                    return RedirectToAction("Profile", "Account");
                }
                else
                {
                    return RedirectToAction("Index", "Home");
                }
            }
            return View();
        }

        [HttpPost]
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
                        return RedirectToAction("Profile", "Employee");
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

        public new ActionResult Profile()
        {
            ViewBag.CostCentreList = new SelectList(_commonDataService.GetAllCostCentre(),"CostCentreCode","CostCentreCode");
            return View((User as UserPrincipal).SerializedData);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public new ActionResult Profile(StaffAccountDto dto)
        {
            //_accountClient.UpdateStaff(dto);
            ViewBag.CostCentreList = new SelectList(_commonDataService.GetAllCostCentre(), "CostCentreCode", "CostCentreCode");
            ViewBag.IsSuccess = false;
            ViewBag.Message = "Update success";
            return View(dto);
        }

        public ActionResult ResetPassword()
        {
            var userData = (User as UserPrincipal).SerializedData;
            ResetPasswordRequestViewModel dto = new ResetPasswordRequestViewModel()
            {
                StaffID = userData.StaffID,
                Name = string.Format("{0} {1}", userData.FirstName, userData.SurName)
            };
            return View(dto);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ResetPassword(ResetPasswordRequestDto dto)
        {

            ViewBag.IsSuccess = false;
            ViewBag.Message = "Update success";
            return View(dto);
        }

        public ActionResult ResetPasswordSuccess()
        {
            return View();
        }
    }
}