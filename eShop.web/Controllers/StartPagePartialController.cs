using EPiServer.Framework.DataAnnotations;
using EPiServer.Framework.Web;
using EPiServer.Web.Mvc;
using eShop.web.Models.Pages;
using System.Web.Mvc;

namespace eShop.web.Controllers
{
    public class StartPagePartialController : BasePartialController<StartPage>
    {
        // GET: StartPagePartial
        public override ActionResult Index(StartPage currentPage)
        {
            return PartialView("PagePartials/Page", currentPage);
        }
    }
}