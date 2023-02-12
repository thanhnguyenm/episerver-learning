using EPiServer.Commerce.Catalog.ContentTypes;
using EPiServer.Framework;
using EPiServer.Framework.Initialization;
using eShop.web.Commerce.Products;
using Mediachase.BusinessFoundation.Data;
using Mediachase.Commerce.Catalog;
using Mediachase.Commerce.Orders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace eShop.web.Business.Initialization
{
    /// <summary>
    /// Module for customizing templates and rendering.
    /// </summary>
    [ModuleDependency(typeof(EPiServer.Web.InitializationModule))]
    public class RemoveOldMetaFieldInitiakization : IInitializableModule
    {
        public void Initialize(InitializationEngine context)
        {
            var metaclass = DataContext.Current.MetaModel.MetaClasses;
            
            //var x = new ProductContent();
            //var metaclassid = x.MetaClassId;
            //var classmeta = metaclass[0];
            //var i = 0;

            //CatalogContext.Current.Me
            
        }

        public void Uninitialize(InitializationEngine context)
        {
            
        }
    }
}