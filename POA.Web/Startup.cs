using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(POA.Web.Startup))]
namespace POA.Web
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
