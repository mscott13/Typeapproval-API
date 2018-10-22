using Microsoft.Owin;
using Owin;

[assembly: OwinStartup(typeof(WebService.Startup), "Configuration")]

namespace WebService.R.configure
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            WebService.Startup.Configuration(app);
        }
    }
}