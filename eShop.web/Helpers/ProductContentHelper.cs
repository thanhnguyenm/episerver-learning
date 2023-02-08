using EPiServer;
using EPiServer.Commerce.Catalog;
using EPiServer.Commerce.Catalog.ContentTypes;
using EPiServer.Commerce.Catalog.Linking;
using EPiServer.Core;
using EPiServer.ServiceLocation;
using EPiServer.Web.Routing;
using eShop.web.ViewModels;
using Mediachase.Commerce.InventoryService;
using Mediachase.Commerce.Markets;
using Mediachase.Commerce.Orders;
using Mediachase.Commerce.Orders.Managers;
using Mediachase.Commerce.Pricing;
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

            var marketService = ServiceLocator.Current.GetInstance<IMarketService>();
            var priceService = ServiceLocator.Current.GetInstance<IPriceService>();
            var priceDetailService = ServiceLocator.Current.GetInstance<IPriceDetailService>();
            var inventoryService = ServiceLocator.Current.GetInstance<IInventoryService>();
            //var linksRepository = ServiceLocator.Current.GetInstance<ILinksRepository>();

            var model = new ProductContentViewModel { ProductName = productContent.Name };

            var variants = productContent.GetVariants();
            
            var variantContents = contentLoader.GetItems(variants, language);
            var variantsCodes = variantContents.Select(x => (x as VariationContent).Code);
            var inventoryRecords = inventoryService.QueryByEntry(variantsCodes);
            model.Quantity = inventoryRecords.Sum(x => x.PurchaseAvailableQuantity);
            

            model.Variants = variantContents.Select(x => new ProductContentViewModel
            {
                Code = (x as VariationContent).Code
            }).ToList();

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


            //check cart
            //var cart = new Cart(Guid.NewGuid().ToString(), Guid.NewGuid());
            //OrderGroupWorkflowManager.RunWorkflow(cart, OrderGroupWorkflowManager.CartValidateWorkflowName);
            //or
            //var result = cart.RunWorkflow(OrderGroupWorkflowManager.CartValidateWorkflowName);

            // request inventory
            //var request = new InventoryRequest(DateTime.UtcNow, requestItems, null);
            //var response = inventoryService.Request(request);

            
            return model;
        }

        public static List<ProductContentViewModel> RelatedProducts(this ProductContent productContent, CultureInfo language)
        {
            var contentLoader = ServiceLocator.Current.GetInstance<IContentLoader>();
            var assetUrlResolver = ServiceLocator.Current.GetInstance<AssetUrlResolver>();
            var contentMediaResolver = ServiceLocator.Current.GetInstance<ContentMediaResolver>();
            var assetUrlConventions = ServiceLocator.Current.GetInstance<AssetUrlConventions>();
            var urlResolver = ServiceLocator.Current.GetInstance<UrlResolver>();
            var relationRepository = ServiceLocator.Current.GetInstance<IRelationRepository>();
            var associationRepository = ServiceLocator.Current.GetInstance<IAssociationRepository>();
            var marketService = ServiceLocator.Current.GetInstance<IMarketService>();
            var priceService = ServiceLocator.Current.GetInstance<IPriceService>();
            var priceDetailService = ServiceLocator.Current.GetInstance<IPriceDetailService>();
            var inventoryService = ServiceLocator.Current.GetInstance<IInventoryService>();
            //var linksRepository = ServiceLocator.Current.GetInstance<ILinksRepository>();
            var refConverter = ServiceLocator.Current.GetInstance<Mediachase.Commerce.Catalog.ReferenceConverter>();

            

            var rs = new List<ProductContentViewModel>();

            var associations = productContent.GetAssociations()
                .Take(4)
                .Select(x => x.Target);

            var associationContentData = contentLoader.GetItems(associations, language);

            foreach(var ass in associationContentData)
            {
                var model = new ProductContentViewModel { ProductName = ass.Name };
                var variants = (ass as ProductContent).GetVariants();
                var variantContents = contentLoader.GetItems(variants, language);

                var prices = variantContents.SelectMany(x => (x as VariationContent).GetPrices());

                if (prices != null && prices.Any())
                {
                    var minPrice = prices.Min(x => x.UnitPrice);
                    var maxPrice = prices.Max(x => x.UnitPrice);

                    model.MaxPrice = maxPrice;
                    model.MinPrice = minPrice;
                }

                model.ProductURL = UrlResolver.Current.GetUrl(ass.ContentLink);

                var url = assetUrlResolver.GetAssetUrl<IContentImage>((ass as ProductContent).CommerceMediaCollection, "default");
                model.Image = url;

                //model.DetailImages = ass.CommerceMediaCollection
                //    .Where(x => x.GroupName == "detail")
                //    .Select(x => urlResolver.GetUrl(x.AssetLink)).ToList();

                rs.Add(model);
            }
            
            return rs;
        }

        private static void SendRequests(Shipment shipment, InventoryRequestType requestType)
        {
            var inventoryService = ServiceLocator.Current.GetInstance<IInventoryService>();
            var itemIndexStart = 0;
            var requestItems = shipment.OperationKeysMap.SelectMany(c => c.Value).Select
                    (key =>
                    new InventoryRequestItem()
                    {
                        ItemIndex = itemIndexStart++,
                        OperationKey = key,
                        RequestType = requestType
                    }).ToList();

            if (requestItems.Any())
            {
                inventoryService.Request(new InventoryRequest(DateTime.UtcNow, requestItems, null));
                shipment.ClearOperationKeys();
            }
        }

    }
}