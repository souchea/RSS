using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(RSSAgregator.Web.Startup))]
namespace RSSAgregator.Web
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
