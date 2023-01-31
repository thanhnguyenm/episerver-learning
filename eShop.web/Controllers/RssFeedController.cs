using EPiServer.DataAbstraction;
using EPiServer.ServiceLocation;
using eShop.web.Business.Services;
using eShop.web.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.WebPages;

namespace eShop.web.Controllers
{
    public class RssFeedController : Controller
    {
        private static Injected<IFeedService> feedService;
        //private static Injected<ContentTypeRepository> contentTypeRepository;

        // GET: RssFeed
        public ActionResult Index()
        {
            
            var feed = feedService.Service.Generate("thanhnmitc", "http://localhost:64340/", "test generate rss feed");


            return new RssActionResult() { Feed = feed };
        }
    }
}