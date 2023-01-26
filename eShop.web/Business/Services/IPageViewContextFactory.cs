using EPiServer.Core;
using eShop.web.ViewModels;
using System.Web.Routing;

namespace eShop.web.Business.Services
{
    public interface IPageViewContextFactory
    {
        LayoutViewModel CreateLayoutModel(ContentReference currentContentLink, RequestContext requestContext);
    }
}
