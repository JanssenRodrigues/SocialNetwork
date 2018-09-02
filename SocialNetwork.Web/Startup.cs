using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(SocialNetwork.Web.Startup))]
namespace SocialNetwork.Web
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
