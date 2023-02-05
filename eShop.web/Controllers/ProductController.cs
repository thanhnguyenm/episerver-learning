using EPiServer.Commerce.Catalog.ContentTypes;
using EPiServer.Globalization;
using EPiServer.Web.Mvc;
using eShop.web.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace eShop.web.Controllers
{
    public class ProductController : ContentController<ProductContent>
    {
        // GET: Product
        public ActionResult Index(ProductContent currentContent)
        {
            var languageValue = ContentLanguage.PreferredCulture;

            var model = new ProductPageViewModel(currentContent, languageValue);

            return View(model);
        }
    }
}