using SwinSchool.CommonShared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace SwinSchool.WebUI.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            SetupRoleDatabase();
            return View();
        }

        private void SetupRoleDatabase()
        {
            var roles = Roles.GetAllRoles();
            if (roles == null || roles.Length == 0)
            {
                CreateRoles();
            }
        }

        private void CreateRoles()
        {
            Roles.CreateRole(Constants.RoleValue.Administrator);
            Roles.CreateRole(Constants.RoleValue.Employee);
        }

    }
}
