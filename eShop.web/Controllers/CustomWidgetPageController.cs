using EPiServer;
using EPiServer.Core;
using EPiServer.Framework.DataAnnotations;
using EPiServer.Web.Mvc;
using eShop.web.Models.Pages;
using eShop.web.ViewModels;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace eShop.web.Controllers
{
    [Authorize(Roles = "WebEditors, WedAdmins, Administrators")]
    public class CustomWidgetPageController : BasePageController<CustomWidgetPage>
    {
        public ActionResult Index(CustomWidgetPage currentPage)
        {
            /* Implementation of action. You can create your own view model class that you pass to the view or
             * you can pass the page type for simpler templates */
            var viewmodel = PageViewModel.Create(currentPage);


            return PartialView("Widgets/_CustomWidgetPage", viewmodel);
            
        }
    }
}