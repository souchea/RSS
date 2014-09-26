using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(RSSAgregator.Server.Startup))]
namespace RSSAgregator.Server
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
