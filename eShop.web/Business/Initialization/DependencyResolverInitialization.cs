using EPiServer.Framework;
using EPiServer.Framework.Initialization;
using EPiServer.ServiceLocation;
using eShop.web.Business.Dependencies;
using System.Web.Mvc;

namespace eShop.web.Business.Initialization
{
    [InitializableModule]
    public class DependencyResolverInitialization : IConfigurableModule
    {
        public void ConfigureContainer(ServiceConfigurationContext context)
        {
            //Implementations for custom interfaces can be registered here.
            context.ConfigurationComplete += (o, e) =>
            {
                //Register custom implementations that should be used in favour of the default implementations
                ServiceRegistration.Register(context.Services);
            };

            // For using Structure Mapp
            //context.StructureMap().Configure(ServiceRegistration.Register);
            //DependencyResolver.SetResolver(new StructureMapDependencyResolver(context.StructureMap()));
        }

        public void Initialize(InitializationEngine context)
        {
            //For using Service Locator, if we use StructureMap, we can remove this line
            DependencyResolver.SetResolver(new ServiceLocatorDependencyResolver(context.Locate.Advanced));

        }

        public void Uninitialize(InitializationEngine context)
        {
        }

        public void Preload(string[] parameters)
        {
        }
    }
}