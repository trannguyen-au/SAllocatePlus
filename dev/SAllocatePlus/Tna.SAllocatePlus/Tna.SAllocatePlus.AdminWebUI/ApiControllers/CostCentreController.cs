using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Tna.SAllocatePlus.AdminWebUI.Security;
using Tna.SAllocatePlus.ClientServices;

namespace Tna.SAllocatePlus.AdminWebUI.ApiControllers
{
    [Route("api/CostCentre")]
    public class CostCentreController : ApiController
    {
        CommonDataServiceClient _commonDataService;

        public CostCentreController()
        {
            _commonDataService = ServiceFactory.CreateCommonDataServiceClient();
        }

        // GET api/<controller>
        [Route("api/CostCentre/{writeAccess}")]
        public IHttpActionResult Get([FromUri]bool writeAccess)
        {
            if (User.Identity.IsAuthenticated && User is UserPrincipal)
            {
                return Ok(writeAccess ? (User as UserPrincipal).WriteAccessCostCentre : (User as UserPrincipal).ReadAccessCostCentre);
            }

            return Ok(new string[] {});
        }

        [Route("api/CostCentre")]
        public IHttpActionResult Get()
        {
            try
            {
                return Ok(_commonDataService.GetAllCostCentre());
            }
            catch (Exception ex)
            {
                return this.NotFound();
            }
        }
    }
}