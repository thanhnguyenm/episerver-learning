using EPiServer;
using EPiServer.Commerce.Catalog;
using EPiServer.Commerce.Catalog.ContentTypes;
using EPiServer.Core;
using EPiServer.Globalization;
using EPiServer.ServiceLocation;
using EPiServer.Web.Routing;
using eShop.web.Models.Blocks;
using eShop.web.ViewModels;
using Mediachase.Commerce.Core;
using Mediachase.Search;
using Mediachase.Search.Extensions;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web.Mvc;

namespace eShop.web.Controllers
{
    public class NewProductBlockController : BaseBlockController<NewProductBlock>
    {
        public override ActionResult Index(NewProductBlock currentBlock)
        {
            var viewmodel = new NewProductViewModel(currentBlock)
            {
                CurrentPage = CurrentPage,
                CurrentPageLink = CurrentPageLink,
                Catalogs = new List<CatalogContentViewModel>()
            };

            var languageValue = ContentLanguage.PreferredCulture;
            var urlResolver = ServiceLocator.Current.GetInstance<UrlResolver>();
            var contentLoader = ServiceLocator.Current.GetInstance<IContentLoader>();
            var refConverter = ServiceLocator.Current.GetInstance<Mediachase.Commerce.Catalog.ReferenceConverter>();
            
            var fashionCatalog = contentLoader.GetChildren<CatalogContent>(refConverter.GetRootLink()).FirstOrDefault(x => x.Name == "Fashion Stuff");
            if(fashionCatalog != null)
            {
                var fashioncatalogs = contentLoader.GetChildren<NodeContent>(fashionCatalog.ContentLink);
                if(fashioncatalogs.Any())
                {
                    var catalogModels = fashioncatalogs.Select(c => new CatalogContentViewModel
                    {
                        CatalogName = c.Name,
                        CatalogReference = c.ContentLink,
                        CatalogUrl = urlResolver.GetUrl(c.ContentLink),
                        Products = GetProductsByCategory(c.ContentLink)
                    });

                    viewmodel.Catalogs.AddRange(catalogModels);
                }    

            }

            //SearchProducts();
            

            return PartialView(viewmodel);
        }

        private ISearchResults SearchProducts()
        {
            CatalogEntrySearchCriteria criteria = new CatalogEntrySearchCriteria();
            criteria.SearchPhrase = "Kimono";
            //criteria.ClassTypes = new CommaDelimitedStringCollection { "ProductContent", "VariationContent" };
            criteria.IsFuzzySearch = true;
            criteria.FuzzyMinSimilarity = 0.7f;
            var manager = new SearchManager(AppContext.Current.ApplicationName);
            var results = manager.Search(criteria);

            return results;
        }

        private List<ProductContentViewModel> GetProductsByCategory(ContentReference contentReference)
        {
            var languageValue = ContentLanguage.PreferredCulture;

            var productModels = new List<ProductContentViewModel>();

            var contentLoader = ServiceLocator.Current.GetInstance<IContentLoader>();
            var assetUrlResolver = ServiceLocator.Current.GetInstance<AssetUrlResolver>();
            var contentMediaResolver = ServiceLocator.Current.GetInstance<ContentMediaResolver>();
            var assetUrlConventions = ServiceLocator.Current.GetInstance<AssetUrlConventions>();
            var urlResolver = ServiceLocator.Current.GetInstance<UrlResolver>();
            var refConverter = ServiceLocator.Current.GetInstance<Mediachase.Commerce.Catalog.ReferenceConverter>();

            var products = contentLoader.GetChildren<ProductContent>(contentReference);
            if (products != null && products.Any())
            {
                foreach (var p in products)
                {
                    var model = new ProductContentViewModel { ProductName = p.Name };
                    var variants = p.GetVariants();

                    var variantContents = contentLoader.GetItems(variants, languageValue);
                    var prices = variantContents.SelectMany(x => (x as VariationContent).GetPrices());

                    if(prices != null && prices.Any())
                    {
                        var minPrice = prices.Min(x => x.UnitPrice);
                        var maxPrice = prices.Max(x => x.UnitPrice);

                        model.MaxPrice = maxPrice;
                        model.MinPrice = minPrice;
                    }
                    
                    model.ProductURL = UrlResolver.Current.GetUrl(p.ContentLink);
                    
                    var url = assetUrlResolver.GetAssetUrl<IContentImage>(p);
                    model.Image = url;
                    productModels.Add(model);
                }
            }

            return productModels;
        }
    }
}


//var languageValue = ContentLanguage.PreferredCulture;

//var productModels = new List<ProductContentViewModel>();

//var contentLoader = ServiceLocator.Current.GetInstance<IContentLoader>();
//var relationRepository = ServiceLocator.Current.GetInstance<IRelationRepository>();
//var referenceConverter = ServiceLocator.Current.GetInstance<ReferenceConverter>();

//var productsNode = contentLoader.GetChildren<NodeContent>(contentReference).FirstOrDefault(x => x.Name == "Products");
//if (productsNode != null)
//{
//    var products = contentLoader.GetChildren<ProductContent>(productsNode.ContentLink);
//    if (products != null && products.Any())
//    {
//        foreach (var p in products)
//        {
//            var model = new ProductContentViewModel { ProductName = p.Name };
//            var variants = relationRepository.GetChildren<ProductVariation>(p.ContentLink).ToList();

//            var prices = variants.SelectMany(x => (x.Child as VariationContent).GetPrices());



//        }
//    }
//}