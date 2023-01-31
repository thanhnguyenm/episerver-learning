using EPiServer;
using EPiServer.Core;
using EPiServer.DataAccess;
using EPiServer.Framework.Blobs;
using EPiServer.Framework.DataAnnotations;
using EPiServer.Security;
using EPiServer.ServiceLocation;
using EPiServer.Web;
using EPiServer.Web.Mvc;
using eShop.web.Models.Media;
using eShop.web.Models.Pages;
using eShop.web.ViewModels;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace eShop.web.Controllers
{
    public class UploadFilePageController : PageController<UploadFilePage>
    {
        public ActionResult Index(UploadFilePage currentPage)
        {
            /* Implementation of action. You can create your own view model class that you pass to the view or
             * you can pass the page type for simpler templates */

            var viewModel = PageViewModel.Create(currentPage);

            return View(viewModel);
        }

        [HttpPost]
        public ActionResult Upload()
        {
            HttpPostedFileBase file = Request.Files[0];
            var filename = Request.Form["Name"];
            string imageExtension = System.IO.Path.GetExtension(filename);
            int pageId = int.Parse(Request.Form["CurrentPage"].ToString());
            var currentPage = new ContentReference(pageId);

            var contentAssetHelper = ServiceLocator.Current.GetInstance<ContentAssetHelper>();
            var contentAssetFolder = contentAssetHelper.GetOrCreateAssetFolder(currentPage);
            
            var contentRepository = ServiceLocator.Current.GetInstance<IContentRepository>();

            var folder = contentRepository.GetChildren<ContentFolder>(contentAssetFolder.ContentLink).Where(f => f.Name == DateTime.Now.ToString("ddMMyyyy")).FirstOrDefault();
            if(folder == null)
            {
                folder = contentRepository.GetDefault<ContentFolder>(contentAssetFolder.ContentLink);
                folder.Name = DateTime.Now.ToString("ddMMyyyy");
                contentRepository.Save(folder, SaveAction.Publish, AccessLevel.NoAccess);
            }
            

            var newImage = contentRepository.GetDefault<ImageFile>(folder.ContentLink);

            newImage.Name = System.IO.Path.GetFileNameWithoutExtension(filename);
            var blobFactory = ServiceLocator.Current.GetInstance<IBlobFactory>();
            var blob = blobFactory.CreateBlob(newImage.BinaryDataContainer, imageExtension);
            blob.Write(file.InputStream);

            newImage.BinaryData = blob;
            contentRepository.Save(newImage, SaveAction.Publish, AccessLevel.NoAccess);

            //Get Image from an property of page
            //var repo = ServiceLocator.Current.GetInstance<IContentRepository>();
            //var image = repo.Get<ImageData>(currentPage.MainLogo);
            //var fileName = image.Name;

            // or
            //var mainLogo = currentPage.GetPropertyValue<ContentReference>("MainLogo");
            //var urlHelper = ServiceLocator.Current.GetInstance<UrlHelper>();
            //var friendlyUrl = urlHelper.ContentUrl(mainLogo);

            // Get Image URL friendly
            // @Url.ContentUrl(Model.CurrentPage.MainLogo)



            return Json("");
        }
    }
}