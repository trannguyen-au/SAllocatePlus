using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Tna.SAllocatePlus.ClientServices;

namespace Tna.SAllocatePlus.AdminWebUI.ApiControllers
{
    [Authorize]
    public class RoleController : ApiController
    {
        CommonDataServiceClient _commonDataService;

        public RoleController()
        {
            _commonDataService = ServiceFactory.CreateCommonDataServiceClient();
        }

        // GET api/Role
        public IHttpActionResult Get()
        {
            return Ok(_commonDataService.GetAllRole());
        }
    }
}