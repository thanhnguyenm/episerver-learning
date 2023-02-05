using EPiServer;
using EPiServer.Commerce.Catalog.ContentTypes;
using EPiServer.Core;
using EPiServer.DataAccess;
using EPiServer.Framework.DataAnnotations;
using EPiServer.ServiceLocation;
using EPiServer.Web.Mvc;
using eShop.web.Models.Pages;
using eShop.web.ViewModels;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace eShop.web.Controllers
{
    public class TestCommercePageController : PageController<TestCommercePage>
    {
        public ActionResult Index(TestCommercePage currentPage)
        {
            /* Implementation of action. You can create your own view model class that you pass to the view or
             * you can pass the page type for simpler templates */

            var model = PageViewModel.Create(currentPage);

            return View(model);
        }

        [HttpPost]
        public JsonResult CreateCatalog(CatalogCreateModel model)
        {
            
            var repository = ServiceLocator.Current.GetInstance<IContentRepository>();

            var refConverter = ServiceLocator.Current.GetInstance<Mediachase.Commerce.Catalog.ReferenceConverter>();
            var catalogRoot = refConverter.GetRootLink();

            var newcatalog = repository.GetDefault<CatalogContent>(catalogRoot);
            newcatalog.Name = model.CatalogName;
            newcatalog.DefaultCurrency = "USD";
            newcatalog.DefaultLanguage = "en";
            newcatalog.WeightBase = "Pounds";

            var catalogRef = repository.Save(newcatalog, SaveAction.Publish, EPiServer.Security.AccessLevel.NoAccess);

            // add language fr
            newcatalog = repository.Get<CatalogContent>(catalogRef).CreateWritableClone<CatalogContent>();
            newcatalog.CatalogLanguages.Add("fr");
            repository.Save(newcatalog, SaveAction.Publish, EPiServer.Security.AccessLevel.NoAccess);


            return Json(newcatalog.CatalogId);
        }
    }
}