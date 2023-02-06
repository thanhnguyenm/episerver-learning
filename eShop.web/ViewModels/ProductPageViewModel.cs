using EPiServer;
using EPiServer.Commerce.Catalog.ContentTypes;
using EPiServer.ServiceLocation;
using EPiServer.Web;
using eShop.web.Helpers;
using eShop.web.Models.Pages;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;

namespace eShop.web.ViewModels
{
    public class ProductPageViewModel
    {
        public ProductPageViewModel(ProductContent currentContent, CultureInfo language)
        {
            CurrentContent = currentContent;
            CurrentContentModel = currentContent.ToModel(language);
            RelatedProducts = currentContent.RelatedProducts(language);
            Language = language.Name;

            var contentLoader = ServiceLocator.Current.GetInstance<IContentLoader>();
            StartPageViewModel = PageViewModel.Create(contentLoader.Get<StartPage>(SiteDefinition.Current.StartPage));


        }

        public ProductContent CurrentContent { get; }
        public string Language { get; }

        public ProductContentViewModel CurrentContentModel { get; }
        public List<ProductContentViewModel> RelatedProducts { get; }

        public IPageViewModel<StartPage> StartPageViewModel { get; }
    }
}