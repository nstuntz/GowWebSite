using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(GowWebSite.Startup))]
namespace GowWebSite
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
