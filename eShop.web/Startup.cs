using Microsoft.Owin;
using Owin;

[assembly: OwinStartup(typeof(eShop.web.Startup))]
namespace eShop.web
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            app.MapSignalR();
            //new EPiServer.ServiceApi.Startup().Configuration(app);
        }
    }
}