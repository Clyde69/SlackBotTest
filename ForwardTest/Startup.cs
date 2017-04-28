using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(ForwardTest.Startup))]
namespace ForwardTest
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
