using System.Web.Routing;
using EPiServer.Commerce.Catalog.ContentTypes;
using EPiServer.Commerce.Catalog.Linking;
using EPiServer.Commerce.Routing;
using EPiServer.Framework;
using EPiServer.Framework.Initialization;
using EPiServer.ServiceLocation;
using EPiServer.Web.Routing;

namespace eShop.web.Infrastructure
{
    [InitializableModule]
    [ModuleDependency(typeof(EPiServer.Commerce.Initialization.InitializationModule))]
    public class EPiServerCommerceInitializationModule : IInitializableModule
    {
        public void Initialize(InitializationEngine context)
        {
            //CatalogRouteHelper.MapDefaultHierarchialRouter(RouteTable.Routes, false);

            CatalogRouteHelper.MapDefaultHierarchialRouter(RouteTable.Routes, true);
            CatalogRouteHelper.SetupSeoUriPermanentRedirect();

            // Create new association group
            var groupAsoDefRepo = context.Locate.Advanced.GetInstance<GroupDefinitionRepository<AssociationGroupDefinition>>();
            groupAsoDefRepo.Add(new AssociationGroupDefinition { Name = "CrossSell" });
        }

        public void Preload(string[] parameters) { }

        public void Uninitialize(InitializationEngine context)
        {
        }

        private static void Routed_SeoUri(object sender, RoutingEventArgs e)
        {
            var context = e.RoutingSegmentContext;
            //RoutedObject is supposed to not be null here
            if (!(context.RoutedObject is CatalogContentBase))
            {
                return;
            }

            if (string.IsNullOrEmpty(context.LastConsumedFragment))
            {
                return;
            }
            var urlResolver = ServiceLocator.Current.GetInstance<UrlResolver>();
            context.PermanentRedirect(urlResolver.GetUrl(context.RoutedContentLink));
        }
    }
}
