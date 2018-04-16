using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(ProjectED1.Startup))]
namespace ProjectED1
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
