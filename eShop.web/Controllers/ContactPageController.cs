using EPiServer;
using EPiServer.Core;
using EPiServer.DataAccess;
using EPiServer.Framework.DataAnnotations;
using EPiServer.Security;
using EPiServer.ServiceLocation;
using EPiServer.Web;
using EPiServer.Web.Mvc;
using eShop.web.Models.Pages;
using eShop.web.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace eShop.web.Controllers
{
    public class ContactPageController : PageController<ContactPage>
    {
        public ActionResult Index(ContactPage currentPage)
        {
            /* Implementation of action. You can create your own view model class that you pass to the view or
             * you can pass the page type for simpler templates */
            var model = PageViewModel.Create(currentPage);

            return View(model);
        }

        [HttpPost]
        public ActionResult Save(UserMessage message, PageReference currentPage)
        {
            var contentRepository = ServiceLocator.Current.GetInstance<IContentRepository>();

            var contentAssetHelper = ServiceLocator.Current.GetInstance<ContentAssetHelper>();

            var contentAssetFolder = contentAssetHelper.GetOrCreateAssetFolder(currentPage);

            var folder = contentRepository.GetChildren<ContentFolder>(SiteDefinition.Current.GlobalAssetsRoot).Where(f => f.Name == DateTime.Now.ToString("ddMMyyyy")).FirstOrDefault();
            if (folder == null)
            {
                folder = contentRepository.GetDefault<ContentFolder>(SiteDefinition.Current.GlobalAssetsRoot);
                folder.Name = DateTime.Now.ToString("ddMMyyyy");
                contentRepository.Save(folder, SaveAction.Publish, AccessLevel.NoAccess);
            }

            var newMessage = contentRepository.GetDefault<eShop.web.Models.UserMessage>(folder.ContentLink);
            newMessage.Name = message.UserName;
            newMessage.UserName = message.UserName;
            newMessage.Email = message.Email;
            newMessage.Message = message.UserComment;

            contentRepository.Save(newMessage, SaveAction.Publish, AccessLevel.NoAccess);

            return Json(message);
        }
    }
}