using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SwinSchool.WebUI.Service
{
    public class ServiceFactory
    {
        public static MyUserBOClient CreateUserBoClient()
        {
            return new MyUserBOClient("wsHttpBinding_IMyUserBO");
        }

        public static UserAccountQueueClient CreateUserAccountQueueClient()
        {
            return new UserAccountQueueClient("UserAccountEndpoint");
        }


    }
}