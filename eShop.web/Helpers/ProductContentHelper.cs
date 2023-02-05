using EPiServer;
using EPiServer.Commerce.Catalog;
using EPiServer.Commerce.Catalog.ContentTypes;
using EPiServer.Core;
using EPiServer.ServiceLocation;
using EPiServer.Web.Routing;
using eShop.web.ViewModels;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;

namespace eShop.web.Helpers
{
    public static class ProductContentHelper
    {
        public static ProductContentViewModel ToModel(this ProductContent productContent, CultureInfo language)
        {
            var contentLoader = ServiceLocator.Current.GetInstance<IContentLoader>();
            var assetUrlResolver = ServiceLocator.Current.GetInstance<AssetUrlResolver>();
            var contentMediaResolver = ServiceLocator.Current.GetInstance<ContentMediaResolver>();
            var assetUrlConventions = ServiceLocator.Current.GetInstance<AssetUrlConventions>();
            var urlResolver = ServiceLocator.Current.GetInstance<UrlResolver>();
            var refConverter = ServiceLocator.Current.GetInstance<Mediachase.Commerce.Catalog.ReferenceConverter>();

            var model = new ProductContentViewModel { ProductName = productContent.Name };

            var variants = productContent.GetVariants();

            var variantContents = contentLoader.GetItems(variants, language);
            var prices = variantContents.SelectMany(x => (x as VariationContent).GetPrices());

            if (prices != null && prices.Any())
            {
                var minPrice = prices.Min(x => x.UnitPrice);
                var maxPrice = prices.Max(x => x.UnitPrice);

                model.MaxPrice = maxPrice;
                model.MinPrice = minPrice;
            }

            model.ProductURL = UrlResolver.Current.GetUrl(productContent.ContentLink);

            var url = assetUrlResolver.GetAssetUrl<IContentImage>(productContent.CommerceMediaCollection, "default");
            model.Image = url;

            model.DetailImages = productContent.CommerceMediaCollection
                .Where(x => x.GroupName == "detail")
                .Select(x => urlResolver.GetUrl(x.AssetLink)).ToList();
            
            return model;
        }
    }
}