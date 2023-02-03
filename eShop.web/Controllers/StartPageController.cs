using EPiServer;
using EPiServer.Core;
using EPiServer.Framework.Cache;
using EPiServer.ServiceLocation;
using EPiServer.Web;
using EPiServer.Web.Mvc;
using eShop.web.Business.Services;
using eShop.web.Models.Pages;
using eShop.web.ViewModels;
using Newtonsoft.Json;
using System.Linq;
using System.Web.Mvc;

namespace eShop.web.Controllers
{
    //[ContentOutputCache]
    public class StartPageController : BasePageController<StartPage>
    {
        private readonly ITestService service;
        private readonly ITestService2 testService2;
        private readonly IContentVersionRepository contentVersionRepository;
        private readonly IContentLoader contentLoader;
        private Injected<ITestService2> iTestService2;

        public StartPageController(
            //ITestService service, 
            //ITestService2 testService2, 
            //IContentVersionRepository contentVersionRepository,
            //IContentLoader contentLoader,
            // IObjectInstanceCache objectInstanceCache
            )
        {
            //this.service = service;
            //this.testService2 = testService2;
            //this.contentVersionRepository = contentVersionRepository;
            //this.contentLoader = contentLoader;

            this.service = ServiceLocator.Current.GetInstance<ITestService>();
            this.testService2 = ServiceLocator.Current.GetInstance<ITestService2>();
            this.contentVersionRepository = ServiceLocator.Current.GetInstance<IContentVersionRepository>();
            this.contentLoader = ServiceLocator.Current.GetInstance<IContentLoader>();
        }

        // GET: StartPage
        public ActionResult Index(StartPage currentPage)
        {
            /* Implementation of action. You can create your own view model class that you pass to the view or
             * you can pass the page type for simpler templates */

            var model = PageViewModel.Create(currentPage);

            //if (SiteDefinition.Current.StartPage.CompareToIgnoreWorkID(currentPage.ContentLink))
            //{
            //    var editHints = ViewData.GetEditHints<PageViewModel<StartPage>, StartPage>();
            //    editHints.AddConnection(m => m.Layout.SiteLogo, p => p.SiteLogo);
            //}

            // Depedencies
            service.Hello();
            testService2.Hello();

            var testSevice22 = iTestService2.Service;
            testSevice22.Hello();

            // Get old versions
            // No cache of this api, be care with website
            var versions = contentVersionRepository.List(currentPage.ContentLink);
            var orderedVerions = versions.OrderByDescending(x => x.Saved).ToList();

            // to identify an item, use ContentReference currentPage.ContentLink (Id, Version, Provider)
            // serial it before store
            var contRefStr = JsonConvert.SerializeObject(currentPage.ContentLink);
            var contentRefObj = JsonConvert.DeserializeObject<ContentReference>(contRefStr);

            // cache checking
            var children = contentLoader.GetChildren<SitePageData>(currentPage.ContentLink);
            
            return View(model);
        }
    }
}