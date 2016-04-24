using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(ProudSource2.Startup))]
namespace ProudSource2
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
