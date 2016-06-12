using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Tna.SAllocatePlus.ClientServices;

namespace Tna.SAllocatePlus.PublicApi
{
    public sealed class ServiceFactory
    {
        public static JobServiceClient CreateJobServiceClient()
        {
            return new JobServiceClient("JobServiceEndPoint");
        }

        public static AccountServiceClient CreateAccountServiceClient()
        {
            return new AccountServiceClient("AccountServiceEndPoint");
        }

        public static CommonDataServiceClient CreateCommonDataServiceClient()
        {
            return new CommonDataServiceClient("CommonDataServiceEndPoint");
        }

    }
}