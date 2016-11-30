using Microsoft.Owin;
using Microsoft.Owin.Security.Cookies;
using Owin;

namespace ProudSourcePrime
{
    /// <summary>
    /// This class sets up the interface that is used with OWIN's auth identity.
    /// </summary>
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            app.UseCookieAuthentication(new CookieAuthenticationOptions
            {
                /// Here we set wht the authentication type that we are going to use. We will use cookies stored on the client's browser and check them on the server.
                AuthenticationType = "ApplicationCookie",
                /// This sets where the login URL for this web app is.
                LoginPath = new PathString("/Auth/Login")
            });
        }
    }
}