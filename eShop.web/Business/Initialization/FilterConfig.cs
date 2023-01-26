using EPiServer.Framework;
using EPiServer.Framework.Initialization;
using EPiServer.ServiceLocation;
using eShop.web.Business.Filters;
using System.Web.Mvc;

namespace eShop.web.Business.Initialization
{
    /// <summary>
    /// Module for registering filters which will be applied to controller actions.
    /// </summary>
    [ModuleDependency(typeof(EPiServer.Web.InitializationModule))]
    public class FilterConfig : IInitializableModule
    {
        public void Initialize(InitializationEngine context)
        {
            GlobalFilters.Filters.Add(ServiceLocator.Current.GetInstance<PageContextActionFilter>());
            //GlobalFilters.Filters.Add(ServiceLocator.Current.GetInstance<BrowserCachingActionFilter>());
        }

        public void Uninitialize(InitializationEngine context)
        {
        }

        public void Preload(string[] parameters)
        {
        }
    }
}