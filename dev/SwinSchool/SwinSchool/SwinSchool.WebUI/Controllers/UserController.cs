using SwinSchool.CommonShared.Dto;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SwinSchool.WebUI.Controllers
{
    public class UserController : Controller
    {
        //Services.MyUserService _myUserService = new Services.MyUserService();
        Services.MyUserService _myUserService = new Services.MyUserService(ConfigurationManager.ConnectionStrings["SchoolContext"].ConnectionString);
        //
        // GET: /User/

        public ActionResult Index()
        {
            return View(_myUserService.GetAllUsers());
        }

        //
        // GET: /User/Create

        public ActionResult Create()
        {
            return View();
        }

        //
        // POST: /User/Create

        [HttpPost]
        public ActionResult Create(MyUserDto myUser)
        {
            try
            {
                _myUserService.CreateUser(myUser);
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        //
        // GET: /User/Edit/5

        public ActionResult Edit(string id)
        {
            return View(_myUserService.GetUserById(id));
        }

        //
        // POST: /User/Edit/5

        [HttpPost]
        public ActionResult Edit(MyUserDto myUser)
        {
            try
            {
                _myUserService.UpdateUser(myUser);
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        //
        // POST: /User/Delete/5

        [HttpPost]
        public ActionResult Delete(string id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
