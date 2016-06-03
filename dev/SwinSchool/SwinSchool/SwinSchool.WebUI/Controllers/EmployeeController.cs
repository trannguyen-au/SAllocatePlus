using SwinSchool.WebUI.Models;
using SwinSchool.WebUI.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SwinSchool.WebUI.Controllers
{
    [Authorize(Roles = "Administrator|Employee")]
    public class EmployeeController : Controller
    {
        MyUserBOClient _myUserService = ServiceFactory.CreateUserBoClient();
        //
        // GET: /User/

        public ActionResult Index()
        {
            var userDetail = (User as SwinSchool.WebUI.Security.UserPrincipal).SerializedData;
            return View(userDetail);
        }

        public ActionResult ResetPassword(string id)
        {
            var user = _myUserService.GetUserById(id);

            ResetPasswordRequestViewModel vm = new ResetPasswordRequestViewModel()
            {
                Name = user.Name,
                UserID = user.UserID
            };

            return View(vm);
        }

        public ActionResult ResetPasswordSuccess()
        {
            return View();
        }
    }
}
