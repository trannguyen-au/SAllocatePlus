using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Tna.SAllocatePlus.AdminWebUI.Models;
using Tna.SAllocatePlus.ClientServices;
using Tna.SAllocatePlus.CommonShared;
using Tna.SAllocatePlus.CommonShared.Dto;

namespace Tna.SAllocatePlus.AdminWebUI.ApiControllers
{
    [Authorize]
    public class StaffController : ApiController
    {
        AccountServiceClient _accountService;

        public StaffController()
        {
            _accountService = ServiceFactory.CreateAccountServiceClient();
        }


        // GET api/<controller>/5
        public IHttpActionResult Get(string costCentre)
        {
            try
            {
                return Ok(_accountService.GetStaffsByCostCentre(costCentre));
            }
            catch (Exception ex)
            {
                return NotFound();
            }
        }

        public IHttpActionResult Get(int id)
        {
            try
            {
                return Ok(_accountService.GetUserById(id));
            }
            catch (Exception ex)
            {
                return NotFound();
            }
        }

        [Authorize]
        [HttpPost]
        public HttpResponseMessage ResetPassword(ResetPasswordRequestViewModel resetPasswordModel)
        {
            var jsonMessage = new JsonResponseMessage();
            if (!ModelState.IsValid)
            {
                jsonMessage.Error("Invalid data", resetPasswordModel);
            }
            else if (resetPasswordModel.NewPassword != resetPasswordModel.ConfirmPassword)
            {
                jsonMessage.Error("Your password doesn't not match", resetPasswordModel);
            }

            if (!jsonMessage.IsSuccess)
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest, jsonMessage);
            }

            try
            {
                var resetPasswordDto = new ResetPasswordRequestDto()
                {
                    OldPassword = EncryptionService.EncryptPassword(resetPasswordModel.OldPassword),
                    StaffID = resetPasswordModel.StaffID,
                    NewPassword = resetPasswordModel.NewPassword
                };

                var errors = _accountService.ResetPassword(resetPasswordDto);

                if (errors.Length > 0)
                {
                    jsonMessage.Error(string.Join("; ", errors));
                    return Request.CreateResponse(HttpStatusCode.BadRequest, jsonMessage);
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