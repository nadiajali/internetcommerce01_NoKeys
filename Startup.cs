using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(internetcommerce01.Startup))]
namespace internetcommerce01
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
