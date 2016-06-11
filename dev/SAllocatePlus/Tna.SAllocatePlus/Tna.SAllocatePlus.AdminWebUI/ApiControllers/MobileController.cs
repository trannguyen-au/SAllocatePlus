using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;
using System.Web.Security;
using Tna.SAllocatePlus.AdminWebUI.Models;
using Tna.SAllocatePlus.AdminWebUI.Security;
using Tna.SAllocatePlus.ClientServices;
using Tna.SAllocatePlus.CommonShared;
using Tna.SAllocatePlus.CommonShared.Dto;

namespace Tna.SAllocatePlus.AdminWebUI.ApiControllers
{
    [Route("api/Mobile")]
    public class MobileController : ApiController
    {

        AccountServiceClient _accountService;
        CommonDataServiceClient _commonDataService;
        JobServiceClient _jobService;
        public MobileController()
        {
            _accountService = ServiceFactory.CreateAccountServiceClient();
            _commonDataService = ServiceFactory.CreateCommonDataServiceClient();
            _jobService = ServiceFactory.CreateJobServiceClient();
        }

        [HttpPost]
        [Route("api/Mobile/ValidateLogin")]
        public HttpResponseMessage ValidateLogin(LoginViewModel vm)
        {
            var jsonMessage = new JsonResponseMessage();
            try
            {
                var userPrincipal = UserPrincipal.ValidateLogin(vm.UserName, vm.Password);
                //var userData = JsonConvert.SerializeObject(userPrincipal.SerializedData);

                //FormsAuthenticationTicket authTicket = new FormsAuthenticationTicket(
                //    1,
                //    userPrincipal.Identity.Name,
                //    DateTime.Now,
                //    DateTime.Now.AddMinutes(30),
                //    false, //pass here true, if you want to implement remember me functionality
                //    userData);

                //string encTicket = FormsAuthentication.Encrypt(authTicket);
                //HttpCookie faCookie = new HttpCookie(FormsAuthentication.FormsCookieName, encTicket);

                jsonMessage.Success("Login success", userPrincipal.SerializedData);
                var response = Request.CreateResponse(HttpStatusCode.OK, jsonMessage);

                return response;
            }
            catch (Exception ex)
            {
                jsonMessage.Error("Login failed: " + ex.Message);
                return Request.CreateResponse(HttpStatusCode.InternalServerError, jsonMessage);
            }
        }

        [Route("api/Mobile/Job/{staffId}")]
        public HttpResponseMessage GetUnsettedJobList([FromUri]int staffId)
        {
            var jsonMessage = new JsonResponseMessage();
            try
            {
                var data = _jobService.GetJobsByStaff(staffId, false);
                jsonMessage.Success("Ok", data);
                return Request.CreateResponse(HttpStatusCode.OK, jsonMessage);

            }
            catch (Exception ex)
            {
                jsonMessage.Error("Login failed: " + ex.Message);
                return Request.CreateResponse(HttpStatusCode.InternalServerError, jsonMessage);
            }

            return null;
        }

        [Route("api/Mobile/Job/1/{staffId}")]
        public HttpResponseMessage GetSettedJobList([FromUri]int staffId)
        {
            var jsonMessage = new JsonResponseMessage();
            try
            {
                var data = _jobService.GetJobsByStaff(staffId, true);
                jsonMessage.Success("Ok", data);
                return Request.CreateResponse(HttpStatusCode.OK, jsonMessage);

            }
            catch (Exception ex)
            {
                jsonMessage.Error("Login failed: " + ex.Message);
                return Request.CreateResponse(HttpStatusCode.InternalServerError, jsonMessage);
            }

            return null;
        }

        [HttpPost]
        [Route("api/Mobile/JobAvailability/{staffId}")]
        public HttpResponseMessage SetJobAvailability([FromUri]int staffId, JobAvailabilityDto jobAvailable)
        {
            var jsonMessage = new JsonResponseMessage();
            try
            {
                _jobService.SetAvailabilityForJob(jobAvailable.BookID, staffId, jobAvailable.IsAvailable.GetValueOrDefault());
                jsonMessage.Success("Ok");
                return Request.CreateResponse(HttpStatusCode.OK, jsonMessage);

            }
            catch (Exception ex)
            {
                jsonMessage.Error("Login failed: " + ex.Message);
                return Request.CreateResponse(HttpStatusCode.InternalServerError, jsonMessage);
            }

            return null;
        }
    }
}
