using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(ProudSourceBeta.Startup))]
namespace ProudSourceBeta
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
