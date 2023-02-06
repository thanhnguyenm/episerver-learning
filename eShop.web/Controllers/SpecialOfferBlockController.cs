using EPiServer;
using EPiServer.Commerce.Catalog;
using EPiServer.Commerce.Catalog.ContentTypes;
using EPiServer.Core;
using EPiServer.Globalization;
using EPiServer.ServiceLocation;
using EPiServer.Web;
using EPiServer.Web.Mvc;
using EPiServer.Web.Routing;
using eShop.web.Models.Blocks;
using eShop.web.ViewModels;
using Mediachase.Commerce.Core;
using Mediachase.Search;
using Mediachase.Search.Extensions;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace eShop.web.Controllers
{
    public class SpecialOfferBlockController : BaseBlockController<SpecialOfferBlock>
    {
        public override ActionResult Index(SpecialOfferBlock currentBlock)
        {
            var viewmodel = new SpecialOfferViewModel(currentBlock)
            {
                CurrentPage = CurrentPage,
                CurrentPageLink = CurrentPageLink
            };
            var languageValue = ContentLanguage.PreferredCulture;

            if (currentBlock.IsPackage)
            {
                viewmodel.ProductContentViewModels = GetPackageSpecialOffers(currentBlock);
            }
            else
            {
                viewmodel.ProductContentViewModels = GetBundleSpecialOffers(currentBlock);
            }
            

            return PartialView(viewmodel);
        }

        private List<ProductContentViewModel> GetPackageSpecialOffers(SpecialOfferBlock currentBlock)
        {
            var contentLoader = ServiceLocator.Current.GetInstance<IContentLoader>();
            var assetUrlResolver = ServiceLocator.Current.GetInstance<AssetUrlResolver>();
            var contentMediaResolver = ServiceLocator.Current.GetInstance<ContentMediaResolver>();
            var assetUrlConventions = ServiceLocator.Current.GetInstance<AssetUrlConventions>();
            var urlResolver = ServiceLocator.Current.GetInstance<UrlResolver>();
            var refConverter = ServiceLocator.Current.GetInstance<Mediachase.Commerce.Catalog.ReferenceConverter>();

            var languageValue = ContentLanguage.PreferredCulture;

            var packagesModels = new List<ProductContentViewModel>();

            var packageCatalog = contentLoader.GetChildren<CatalogContent>(refConverter.GetRootLink()).FirstOrDefault(x => x.Name == "Packages");
            if (packageCatalog != null)
            {
                var childpacakgecatalogs = contentLoader.GetChildren<NodeContent>(packageCatalog.ContentLink).OrderByDescending(x => x.Changed);

                foreach (var node in childpacakgecatalogs)
                {
                    var packages = contentLoader.GetChildren<PackageContent>(node.ContentLink);
                    foreach(var p in packages)
                    {
                        var entries = p.GetEntries();
                        var entriesContents = contentLoader.GetItems(entries, languageValue);
                        //var productContents = contentLoader.GetItems(entriesContents.Select(x => x.ParentLink), languageValue);
                        var prices = p.GetPrices();

                        packagesModels.Add(new ProductContentViewModel
                        {
                            ProductName = string.Join(" & ", entriesContents.Select(x => x.Name)),
                            MinPrice = prices.Min(x => x.UnitPrice),
                            MaxPrice = prices.Max(x => x.UnitPrice)
                        });
                    }
                }
            }

            //CatalogEntrySearchCriteria criteria = new CatalogEntrySearchCriteria();
            //criteria.SearchPhrase = "package";
            //criteria.CatalogNames.Add("Packages");
            //criteria.ClassTypes.Add("PackageContent");

            ////criteria.IsFuzzySearch = true;
            ////criteria.FuzzyMinSimilarity = 0.7f;
            //var manager = new SearchManager(AppContext.Current.ApplicationName);
            //var results = manager.Search(criteria);

            return packagesModels;
        }

        private List<ProductContentViewModel> GetBundleSpecialOffers(SpecialOfferBlock currentBlock)
        {
            var contentLoader = ServiceLocator.Current.GetInstance<IContentLoader>();
            var assetUrlResolver = ServiceLocator.Current.GetInstance<AssetUrlResolver>();
            var contentMediaResolver = ServiceLocator.Current.GetInstance<ContentMediaResolver>();
            var assetUrlConventions = ServiceLocator.Current.GetInstance<AssetUrlConventions>();
            var urlResolver = ServiceLocator.Current.GetInstance<UrlResolver>();
            var refConverter = ServiceLocator.Current.GetInstance<Mediachase.Commerce.Catalog.ReferenceConverter>();

            var languageValue = ContentLanguage.PreferredCulture;

            var packagesModels = new List<ProductContentViewModel>();

            var packageCatalog = contentLoader.GetChildren<CatalogContent>(refConverter.GetRootLink()).FirstOrDefault(x => x.Name == "Bundles");
            if (packageCatalog != null)
            {
                var childpacakgecatalogs = contentLoader.GetChildren<NodeContent>(packageCatalog.ContentLink).OrderByDescending(x => x.Changed);

                foreach (var node in childpacakgecatalogs)
                {
                    var packages = contentLoader.GetChildren<BundleContent>(node.ContentLink);
                    foreach (var p in packages)
                    {
                        var entries = p.GetEntries();
                        var entriesContents = contentLoader.GetItems(entries, languageValue);
                        var prices = entriesContents.Select(x => (x as VariationContent).GetPrices().FirstOrDefault());

                        packagesModels.Add(new ProductContentViewModel
                        {
                            ProductName = string.Join(" & ", entriesContents.Select(x => x.Name)),
                            MinPrice = prices.Where(x => x != null).Sum(x => x.UnitPrice),
                            MaxPrice = prices.Where(x => x != null).Sum(x => x.UnitPrice)
                        }); ;
                    }
                }
            }

            return packagesModels;
        }

    }
}
