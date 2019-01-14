using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(HopesPartners_alpha.Startup))]
namespace HopesPartners_alpha
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
