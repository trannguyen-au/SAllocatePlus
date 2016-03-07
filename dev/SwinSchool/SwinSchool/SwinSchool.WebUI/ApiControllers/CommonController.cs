using SwinSchool.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web.Http;

namespace SwinSchool.WebUI.ApiControllers
{
    public class CommonController : ApiController
    {
        private MyUserService service;

        public CommonController()
        {
            
        }

        public CommonController(ISchoolContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IEnumerable<MyUser> GetAllUsers()
        {
            return _context.MyUsers.ToList();
        }
    }
}
