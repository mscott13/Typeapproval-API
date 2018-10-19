using Microsoft.Owin;
using Owin;
using WebService;

[assembly: OwinStartup(typeof(WebService.Startup))]
namespace WebService
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            app.MapSignalR();
        }
    }
}