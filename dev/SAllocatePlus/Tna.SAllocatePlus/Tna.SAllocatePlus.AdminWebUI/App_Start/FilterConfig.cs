using System.Web;
using System.Web.Mvc;

namespace Tna.SAllocatePlus.AdminWebUI
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
        }
    }
}
