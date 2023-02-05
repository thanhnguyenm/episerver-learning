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
    public class BlogPageController : PageController<BlogPage>
    {
        public ActionResult Index(BlogPage currentPage)
        {
            /* Implementation of action. You can create your own view model class that you pass to the view or
             * you can pass the page type for simpler templates */
            var model = PageViewModel.Create(currentPage);

            return View(model);
        }
    }
}