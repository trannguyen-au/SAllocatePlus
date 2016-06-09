using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Tna.SAllocatePlus.ClientServices;
using Tna.SAllocatePlus.CommonShared.Dto;

namespace Tna.SAllocatePlus.AdminWebUI.ApiControllers
{
    [Route("api/Job")]
    public class JobController : ApiController
    {
        JobServiceClient client;

        public JobController()
        {
            client = new JobServiceClient("JobServiceEndPoint");
        }

        public IHttpActionResult Get(int id)
        {
            return Ok(new JobDto()
            {
                BookID = 213
            });
        }

        public IHttpActionResult Get([FromUri]string costCentre)
        {
            return Ok(client.GetJobsByCostCentre(costCentre).ToList());
        }
    }
}
