using EPiServer.Framework;
using EPiServer.Framework.Initialization;
using EPiServer.ServiceLocation;
using EPiServer.Web.Hosting;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Web;

namespace eShop.web.Business.Initialization
{
    [InitializableModule]
    [ModuleDependency(typeof(EPiServer.Web.InitializationModule))]
    public class ChangingLoginImagesModule : IInitializableModule
    {
        public void Initialize(InitializationEngine context)
        {
            var host = ServiceLocator.Current.GetInstance<IHostingEnvironment>();
            if (host == null)
                return;

            var virtualPathMappedProvider = new VirtualPathMappedProvider("LoginImage", new NameValueCollection());
            //virtualPathMappedProvider.PathMappings.Add("/Util/images/login/Pictures_Page_1-min.jpg", "/Static/img/logo.png"); //=> background
            //virtualPathMappedProvider.PathMappings.Add("/Util/images/login/Pictures_Page_2-min.jpg", "/Static/img/logo.png"); //=> background
            //virtualPathMappedProvider.PathMappings.Add("/Util/images/login/Pictures_Page_3-min.jpg", "/Static/img/logo.png"); //=> background
            virtualPathMappedProvider.PathMappings.Add("/Util/images/favicon.ico", "/favicon.ico"); // admin logo image
            virtualPathMappedProvider.PathMappings.Add("/Util/images/episerver-white.svg", "/Static/img/logo.svg"); // admin logo image
            host.RegisterVirtualPathProvider(virtualPathMappedProvider);
        }

        public void Uninitialize(InitializationEngine context)
        {
        }

        public void Preload(string[] parameters)
        {
        }

    }
}