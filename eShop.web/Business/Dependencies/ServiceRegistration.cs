using EPiServer.ServiceLocation;
using EPiServer.Web.Mvc;
using EPiServer.Web.Mvc.Html;
using eShop.web.Business.Rendering;
using eShop.web.Business.Services;
using StructureMap;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace eShop.web.Business.Dependencies
{
    public static class ServiceRegistration
    {
        public static void Register(IServiceConfigurationProvider services)
        {
            services.AddTransient<ITestService, TestService>();
            services.AddTransient<ContentAreaRenderer, CustomContentAreaRenderer>();
            services.AddScoped<IContentRenderer, ErrorHandlingContentRenderer>();
            services.AddScoped<IFeedService, FeedService>();
            services.AddStackExchangeRedisExtensions();
            services.AddSingleton<CookieService>();

            //services.AddAzureBlobProvider("Azure", o => CMS 12
            // {
            //     o.ConnectionString = { The Azuer storage connection string};
            //     o.ContainerName = { The container name};
            // });
        }


        public static void Register(ConfigurationExpression container)
        {
            container.Scan(scan =>
            {
                scan.TheCallingAssembly();
                scan.WithDefaultConventions();
                scan.Assembly("JonDJones.Website");
                scan.Assembly("JonDJones.Website.Shared");
                scan.Assembly("JonDJones.Website.Core");
            });
        }
    }
}