using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Tna.SAllocatePlus.AdminWebUI.Security;

namespace Tna.SAllocatePlus.AdminWebUI.ApiControllers
{
    [Route("api/CostCentre")]
    public class CostCentreController : ApiController
    {
        // GET api/<controller>
        public IHttpActionResult Get(bool writeAccess)
        {
            if (User.Identity.IsAuthenticated && User is UserPrincipal)
            {
                return Ok(writeAccess ? (User as UserPrincipal).WriteAccessCostCentre : (User as UserPrincipal).ReadAccessCostCentre);
            }

            return Ok(new string[] {});
        }
    }
}