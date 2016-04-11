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

namespace SwinSchool.WebUI.Controllers
{
    public class UserController : Controller
    {
        MyUserBOClient _myUserService = new MyUserBOClient("BasicHttpBinding_IMyUserBO");

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
                SecQn = user.SecQn,
                UserID = user.UserID
            };
            
            return View(vm);
        }

        [HttpPost]
        public ActionResult ResetPassword(ResetPasswordRequestViewModel resetPasswordModel)
        {
            if(!ModelState.IsValid)
                return View(resetPasswordModel);

            try
            {
                var resetPasswordDto = new ResetPasswordRequestDto()
                {
                    SecAns = resetPasswordModel.SecAns,
                    UserId = resetPasswordModel.UserID
                };

                _myUserService.ResetPassword(resetPasswordDto);
                return RedirectToAction("ResetPasswordSuccess");
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("SecAns", ex);

                return View(resetPasswordModel);
            }

        }

        public ActionResult ResetPasswordSuccess()
        {
            return View();
        }
    }
}
