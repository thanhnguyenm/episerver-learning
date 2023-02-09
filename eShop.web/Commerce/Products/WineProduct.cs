using EPiServer.Commerce.Catalog.ContentTypes;
using EPiServer.Commerce.Catalog.DataAnnotations;
using EPiServer.DataAnnotations;
using EPiServer.Shell.ObjectEditing;
using eShop.web.Business.SelectionFactories;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace eShop.web.Commerce.Products
{
    [CatalogContentType(
        GUID = "B6B12938-DF79-47F7-86EB-CE4B765B07BE",
        MetaClassName = "WineProduct",
        DisplayName = "Wine product",
        Description = "Display wine product")]
    public class WineProduct : ProductContent
    {
        [Searchable]
        [CultureSpecific]
        [Tokenize]
        [IncludeInDefaultSearch]
        [Display(Name = "Country", Order = 1)]
        [SelectOne(SelectionFactoryType = typeof(CountriesFactory))]
        public virtual string Country { get; set; }
    }
}