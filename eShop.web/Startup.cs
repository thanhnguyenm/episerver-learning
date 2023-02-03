using eShop.web.Business.Filters;
using eShop.web.Business.HangfireJob;
using Hangfire;
using Microsoft.Owin;
using Owin;
using System.Configuration;

[assembly: OwinStartup(typeof(eShop.web.Startup))]
namespace eShop.web
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            app.MapSignalR();
            //new EPiServer.ServiceApi.Startup().Configuration(app);

            var connection = ConfigurationManager.ConnectionStrings["EPiServerDB"].ConnectionString;
            GlobalConfiguration.Configuration.UseSqlServerStorage(connection);


            var dashboardOptions = new DashboardOptions
            {
                Authorization = new[]
                {
                    new HangFireDashboardAttribute()
                }
            };

            // app.UseHangfireDashboard(); => default 
            app.UseHangfireDashboard("/episerver/backoffice/Plugins/hangfire", dashboardOptions);
            app.UseHangfireServer();

            SampleHangfireJob.TenSecondJob();
        }
    }
}