using SwinSchool.CommonShared.Dto;
using SwinSchool.WebUI.Models;
using SwinSchool.WebUI.Service;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Formatting;
using System.ServiceModel.MsmqIntegration;
using System.Transactions;
using System.Web.Http;

namespace SwinSchool.WebUI.ApiControllers
{
    public class UserApiController : ApiController
    {
        MyUserBOClient _myUserService = null;

        UserAccountQueueClient _myUserAccountQueueService = null;

        public UserApiController()
        {
            _myUserService = new MyUserBOClient("wsHttpBinding_IMyUserBO");
            _myUserAccountQueueService = new UserAccountQueueClient("UserAccountEndpoint");
        }

        [HttpPost]
        public HttpResponseMessage ResetPassword(ResetPasswordRequestViewModel resetPasswordModel)
        {
            var jsonMessage = new JsonResponseMessage();
            if (!ModelState.IsValid)
            {
                jsonMessage.Error("Invalid data", resetPasswordModel);
                return Request.CreateResponse(HttpStatusCode.BadRequest, jsonMessage);
            }

            try
            {


                var resetPasswordDto = new ResetPasswordRequestDto()
                {
                    SecAns = resetPasswordModel.SecAns,
                    UserId = resetPasswordModel.UserID
                };

                var errors = _myUserService.PrecheckForResetPassword(resetPasswordDto);

                if (errors.Length > 0)
                {
                    jsonMessage.Error(string.Join("; ", errors));
                    return Request.CreateResponse(HttpStatusCode.BadRequest, jsonMessage);
                }

                // create a message to do password reset via MSMQ
                MsmqMessage<ResetPasswordRequestDto> resetPwdMsg = new MsmqMessage<ResetPasswordRequestDto>(resetPasswordDto);
                using (TransactionScope scope = new TransactionScope(TransactionScopeOption.Required))
                {
                    _myUserAccountQueueService.SendPasswordResetMessage(resetPwdMsg);
                    scope.Complete();
                }

                jsonMessage.Success("Ok", resetPasswordModel);
                return Request.CreateResponse(HttpStatusCode.Accepted, jsonMessage);
            }
            catch (Exception ex)
            {
                jsonMessage.Error("Server error: " + ex.Message);
                return Request.CreateResponse(HttpStatusCode.InternalServerError, jsonMessage);
            }
        }
    }
}
