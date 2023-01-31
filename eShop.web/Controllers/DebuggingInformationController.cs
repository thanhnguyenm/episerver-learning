using EPiServer.Core;
using EPiServer.Web.Routing;
using eShop.web.Models.Pages;
using eShop.web.ViewModels;
using System.Web.Mvc;

namespace eShop.web.Controllers
{
    public class DebuggingInformationController : Controller
    {
        // GET: DebuggingInformation
        [Authorize(Roles = "WebEditors, WebAdmins, Administrators")]
        public ActionResult Index()
        {
            var pageRouteHelper = EPiServer.ServiceLocation.ServiceLocator.Current.GetInstance<IPageRouteHelper>();
            var viewModel = PageViewModel.Create(pageRouteHelper.Page as SitePageData);

            return View("Index", viewModel);
        }
    }
}