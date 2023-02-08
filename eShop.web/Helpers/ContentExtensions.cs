using EPiServer;
using EPiServer.Commerce.Catalog.ContentTypes;
using EPiServer.Commerce.Catalog.Linking;
using EPiServer.Core;
using EPiServer.Filters;
using EPiServer.Framework.Web;
using EPiServer.ServiceLocation;
using EPiServer.Web.Routing;
using System.Collections.Generic;
using System.Linq;

namespace eShop.web.Helpers
{
    /// <summary>
    /// Extension methods for content
    /// </summary>
    public static class ContentExtensions
    {
        /// <summary>
        /// Filters content which should not be visible to the user.
        /// </summary>
        public static IEnumerable<T> FilterForDisplay<T>(this IEnumerable<T> contents, bool requirePageTemplate = false, bool requireVisibleInMenu = false)
            where T : IContent
        {
            var accessFilter = new FilterAccess();
            var publishedFilter = new FilterPublished();
            contents = contents.Where(x => !publishedFilter.ShouldFilter(x) && !accessFilter.ShouldFilter(x));
            if (requirePageTemplate)
            {
                var templateFilter = ServiceLocator.Current.GetInstance<FilterTemplate>();
                templateFilter.TemplateTypeCategories = TemplateTypeCategories.Page;
                contents = contents.Where(x => !templateFilter.ShouldFilter(x));
            }
            if (requireVisibleInMenu)
            {
                contents = contents.Where(x => VisibleInMenu(x));
            }
            return contents;
        }

        private static bool VisibleInMenu(IContent content)
        {
            var page = content as PageData;
            if (page == null)
            {
                return true;
            }
            return page.VisibleInMenu;
        }

        public static string GetUrl(this EntryContentBase entry, IRelationRepository relationRepository, UrlResolver urlResolver) => GetUrl(entry, relationRepository, urlResolver, null);

        public static string GetUrl(this EntryContentBase entry, IRelationRepository relationRepository, UrlResolver urlResolver, string language)
        {
            var productLink = entry is VariationContent ?
                entry.GetParentProducts(relationRepository).FirstOrDefault() :
                entry.ContentLink;

            if (productLink == null)
            {
                return string.Empty;
            }

            var urlBuilder = string.IsNullOrEmpty(language) ? new UrlBuilder(urlResolver.GetUrl(productLink)) : new UrlBuilder(urlResolver.GetUrl(productLink, language));

            if (entry.Code != null)
            {
                urlBuilder.QueryCollection.Add("variationCode", entry.Code);
            }

            return urlBuilder.ToString();
        }
    }
}