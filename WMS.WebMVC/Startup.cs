using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(WMS.WebMVC.Startup))]
namespace WMS.WebMVC
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
