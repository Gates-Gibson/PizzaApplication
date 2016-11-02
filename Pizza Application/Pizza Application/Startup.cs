using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Pizza_Application.Startup))]
namespace Pizza_Application
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
