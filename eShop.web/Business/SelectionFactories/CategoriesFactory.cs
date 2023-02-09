using EPiServer;
using EPiServer.Core;
using EPiServer.ServiceLocation;
using EPiServer.Shell.ObjectEditing;
using eShop.web.Models.Pages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace eShop.web.Business.SelectionFactories
{
    public class CategoriesFactory : ISelectionFactory
    {
        public Injected<IContentRepository> contentRepository;

        public virtual IEnumerable<ISelectItem> GetSelections(ExtendedMetadata metadata)
        {
            return GetCategories().Select(x => (new SelectItem { Text = x.CategoryName, Value = x.ContentLink.ID.ToString() }));
        }

        protected IEnumerable<BlogCategoryPage> GetCategories()
        {
            var categoryRoot = GetCategoryRootPage();

            var pages = contentRepository.Service.GetChildren<BlogCategoryPage>(categoryRoot.ContentLink);
            return pages;
        }

        private BlogCategoryRootPage GetCategoryRootPage()
        {
            var rootPage = contentRepository.Service.GetChildren<BlogCategoryRootPage>(ContentReference.StartPage).FirstOrDefault();

            if (rootPage == null)
                throw new InvalidOperationException();

            return rootPage;
        }
    }
}