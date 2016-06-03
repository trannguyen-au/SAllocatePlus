using Newtonsoft.Json;
using SwinSchool.CommonShared.Dto;
using SwinSchool.WebUI.Models;
using SwinSchool.WebUI.Security;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using System.Web.Security;

namespace SwinSchool.WebUI
{
    // Note: For instructions on enabling IIS6 or IIS7 classic mode, 
    // visit http://go.microsoft.com/?LinkId=9394801

    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();

            WebApiConfig.Register(GlobalConfiguration.Configuration);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
        }

        protected void Application_PostAuthenticateRequest(Object sender, EventArgs e)
        {
            HttpCookie authCookie = Request.Cookies[FormsAuthentication.FormsCookieName];
            if (authCookie != null)
            {

                FormsAuthenticationTicket authTicket = FormsAuthentication.Decrypt(authCookie.Value);

                MyUserDto serializeModel = JsonConvert.DeserializeObject<MyUserDto>(authTicket.UserData);
                if (serializeModel == null)
                {
                    FormsAuthentication.SignOut();
                    return;
                }
                UserPrincipal newUser = new UserPrincipal(serializeModel);
                HttpContext.Current.User = newUser;
            }

        }
    }
}