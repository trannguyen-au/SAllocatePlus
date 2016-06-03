using SwinSchool.CommonShared.Dto;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.ServiceModel;
using SwinSchool.WebUI.Service;
using SwinSchool.WebUI.Models;
using System.ServiceModel.MsmqIntegration;
using System.Transactions;

namespace SwinSchool.WebUI.Controllers
{
    [Authorize(Roles ="Administrator")]
    public class AdminController : Controller
    {
        MyUserBOClient _myUserService = ServiceFactory.CreateUserBoClient();

        //
        // GET: /User/

        public ActionResult Index()
        {
            List<MyUserDto> allUserList = new List<MyUserDto>();
            allUserList = new List<MyUserDto>(_myUserService.GetAllUsers());

            return View(allUserList);
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
        public ActionResult Edit(MyUserDto myUser, FormCollection collection)
        {
            try
            {
                if(collection["hfAction"]=="Save")
                {
                    _myUserService.UpdateUser(myUser);
                }   
                else if (collection["hfAction"] == "Delete")
                {
                    _myUserService.DeleteUser(myUser.UserID);
                }
                return RedirectToAction("Index");
            }
            catch(Exception ex)
            {
                return View(myUser);
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
            catch (Exception ex)
            {
                return View();
            }
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
