using EPiServer;
using EPiServer.Core;
using EPiServer.Web.Mvc;
using eShop.web.Models.Blocks;
using eShop.web.Models.Pages;
using eShop.web.ViewModels;
using System.Linq;
using System.Web.Mvc;

namespace eShop.web.Controllers
{
    //[ContentOutputCache]
    public class DemoFormPageController : BasePageController<DemoFormPage>
    {
        private readonly IContentLoader contentLoader;

        public DemoFormPageController(IContentLoader contentLoader)
        {
            this.contentLoader = contentLoader;
        }

        public ActionResult Index(DemoFormPage currentPage)
        {
            /* Implementation of action. You can create your own view model class that you pass to the view or
             * you can pass the page type for simpler templates */
            var model = PageViewModel.Create(currentPage);

            return View(model);
        }

        [HttpPost]
        public ActionResult Save(ShippingAddress address, SitePageData page)
        {

            if (this.PageContext?.Page != null)
            {
                var mainContentArea = this.PageContext.Page.GetPropertyValue<ContentArea>("MainContentArea");

                foreach (var item in mainContentArea.Items)
                {
                    var shippingBlock = contentLoader.Get<ShippingAddressBlock>(item.ContentLink);

                    if (shippingBlock != null)
                    {
                        shippingBlock.Address = address;
                    }
                }
            }
            return null;
        }
    }
}