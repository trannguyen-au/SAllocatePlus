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
            client = ServiceFactory.CreateJobServiceClient();
        }

        [Route("api/Job/{id}")]
        public IHttpActionResult Get(int id)
        {
            //return Ok();
            return Ok(client.GetJobById(id));
        }

        public IHttpActionResult Get([FromUri]string costCentre)
        {
            return Ok(client.GetJobsByCostCentre(costCentre).ToList());
        }

        [HttpPost]
        [Route("api/Job/{id}")]
        public IHttpActionResult UpdateJob(JobDto job, int id)
        {
            try
            {
                client.UpdateJob(job);
                // return a fresh entity
                return Ok(client.GetJobById(id));
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }

        public IHttpActionResult Post([FromUri]int id,[FromBody]JobDto job)
        {
            return Ok(job);
        }

        [HttpGet]
        [Route("api/Job/{id}/Availability")]
        public IHttpActionResult GetJobAvailability(int id)
        {
            return Ok(client.GetJobAvailabilityById(id));
        }
    }
}
