using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Tna.SAllocatePlus.ClientServices;
using Tna.SAllocatePlus.AdminWebUI.Models;
using Tna.SAllocatePlus.CommonShared.Dto;

namespace Tna.SAllocatePlus.AdminWebUI.Controllers
{
    [Authorize(Roles = "Administrator")]
    public class JobController : Controller
    {
        JobServiceClient _client;

        public JobController()
        {
            _client = ServiceFactory.CreateJobServiceClient();
        }

        // GET: Job
        public ActionResult Index()
        {
            return View();
        }

        [Route("/Job/GetEmailTemplate/{CostCentre}")]
        public JsonResult GetEmailTemplate(string CostCentre)
        {
            try
            {
                var templateContent = "";
                using (var sr = new StreamReader(Server.MapPath("~/Templates/Emails/SendJobEmail-" + CostCentre + ".txt")))
                {
                    templateContent = sr.ReadToEnd();
                }
                return Json(new { Content = templateContent }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new
                {
                    Success = false,
                    Message = "Cannot find the template for " + CostCentre,
                    Content = ""
                }, JsonRequestBehavior.AllowGet);
            }
        }

        [Route("/Job/SendEmail/")]
        [HttpPost]
        public JsonResult SendEmail(SendEmailRequestDto request)
        {
            try
            {
                _client.SendJobEmail(request);
                return Json(new
                {
                    Success = true,
                    Message = "OK"
                });
            }
            catch (Exception ex)
            {
                return Json(new
                {
                    Success = false,
                    Message = "Failed"
                });
            }
        }
    }
}