using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Connected.Startup))]
namespace Connected
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
