using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Tna.SAllocatePlus.ClientServices;

namespace Tna.SAllocatePlus.AdminWebUI.ApiControllers
{
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
    }
}