using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;
using System.Web.Security;
using Tna.SAllocatePlus.ClientServices;
using Tna.SAllocatePlus.CommonShared;
using Tna.SAllocatePlus.CommonShared.Dto;
using Tna.SAllocatePlus.PublicApi.Models;

namespace Tna.SAllocatePlus.PublicApi.ApiControllers
{
    [Route("api")]
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
        [Route("api/Login")]
        public HttpResponseMessage ValidateLogin(LoginViewModel vm)
        {
            var jsonMessage = new JsonResponseMessage();
            try
            {
                var cyperPass = EncryptionService.EncryptPassword(vm.Password);
                var clientProxy = ServiceFactory.CreateAccountServiceClient();

                var userData = clientProxy.ValidateLogin(vm.UserName, cyperPass);

                if (userData == null)
                {
                    jsonMessage.Error("Login failed");
                    return Request.CreateResponse(HttpStatusCode.BadRequest, jsonMessage);
                }

                jsonMessage.Success("Login success", userData);
                var response = Request.CreateResponse(HttpStatusCode.OK, jsonMessage);

                return response;
            }
            catch (Exception ex)
            {
                jsonMessage.Error("Login failed: " + ex.Message);
                return Request.CreateResponse(HttpStatusCode.InternalServerError, jsonMessage);
            }
        }

        [Route("api/Job/{staffId}")]
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

        [Route("api/Job/1/{staffId}")]
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
        }

        [HttpPost]
        [Route("api/JobAvailability/{staffId}")]
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
        }
    }
}
