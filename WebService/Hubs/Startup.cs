using Owin;
using Microsoft.AspNet.SignalR;
using Microsoft.Owin.Cors;

namespace WebService
{
    public class Startup
    {
        public static void Configuration(IAppBuilder app)
        {
            app.UseCors(CorsOptions.AllowAll);
            app.MapSignalR(new HubConfiguration { EnableJSONP = true});
        }
    }
}

