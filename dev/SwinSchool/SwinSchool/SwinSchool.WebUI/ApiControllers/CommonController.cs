using SwinSchool.CommonShared.Dto;
using SwinSchool.Services;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net;
using System.Web.Http;

namespace SwinSchool.WebUI.ApiControllers
{
    public class CommonController : ApiController
    {
        private MyUserService _service = new MyUserService(ConfigurationManager.ConnectionStrings["SchoolContext"].ConnectionString);

        public CommonController()
        {
            
        }

        [HttpGet]
        public IEnumerable<MyUserDto> GetAllUsers()
        {
            return _service.GetAllUsers();
        }
    }
}
