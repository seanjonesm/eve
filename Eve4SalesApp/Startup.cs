using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Eve4SalesApp.Startup))]
namespace Eve4SalesApp
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
