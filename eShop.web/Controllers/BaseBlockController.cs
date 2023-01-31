using EPiServer.Core;
using EPiServer.ServiceLocation;
using EPiServer.Web.Mvc;
using EPiServer.Web.Routing;
using eShop.web.Models.Pages;

namespace eShop.web.Controllers
{
    public class BaseBlockController<TBlockData> : BlockController<TBlockData> where TBlockData : BlockData
    {
       
        private Injected<IPageRouteHelper> routeHelper;

        protected IPageRouteHelper PageRouteHelper => routeHelper.Service;

        protected PageData CurrentPage
        {
            get
            {
                return routeHelper.Service.Page;
            }
        }

        protected PageReference CurrentPageLink
        {
            get
            {
                return routeHelper.Service.PageLink;
            }
        }
    }
}