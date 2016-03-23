using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SwinSchool.WebUI.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            Services.MyUserService service = new Services.MyUserService();
            
            return View(service.GetAllUsers());
        }

    }
}
