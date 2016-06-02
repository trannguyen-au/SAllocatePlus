using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Tna.SAllocatePlus.ClientServices;

namespace Tna.SAllocatePlus.AdminWebUI.Controllers
{
    public class JobController : Controller
    {
        JobServiceClient _jobServiceClient;

        public JobController()
        {
            _jobServiceClient = new JobServiceClient("JobServiceEndPoint");
        }

        // GET: Job
        public ActionResult Index()
        {
            var jobList = _jobServiceClient.GetJobsByRegion("AU-VIC");

            return View(jobList);
        }
    }
}