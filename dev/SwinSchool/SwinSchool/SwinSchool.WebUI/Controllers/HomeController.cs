using SwinSchool.DAL;
using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SwinSchool.WebUI.Controllers
{

    [Export]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    public class HomeController : Controller
    {
        private ISchoolContext _context;

        [ImportingConstructor]
        public HomeController(ISchoolContext context)
        {
            _context = context;
        }
        //
        // GET: /Home/

        public ActionResult Index()
        {

            return View(_context.MyUsers.ToList());
        }

    }
}
