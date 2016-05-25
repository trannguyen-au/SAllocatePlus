using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Tna.SAllocatePlus.AdminWebUI.Startup))]
namespace Tna.SAllocatePlus.AdminWebUI
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
