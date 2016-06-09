using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Tna.SAllocatePlus.ClientServices;

namespace Tna.SAllocatePlus.AdminWebUI.Controllers
{
    [Authorize(Roles="Administrator")]
    public class JobController : Controller
    {
        JobServiceClient _jobServiceClient;

        public JobController()
        {
            _jobServiceClient = ServiceFactory.CreateJobServiceClient();
        }

        // GET: Job
        public ActionResult Index()
        {
            var jobList = _jobServiceClient.GetJobsByCostCentre("AU-VIC");

            return View(jobList);
        }
    }
}