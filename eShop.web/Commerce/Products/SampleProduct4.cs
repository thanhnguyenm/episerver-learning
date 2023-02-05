using EPiServer.Commerce.Catalog.ContentTypes;
using EPiServer.Commerce.Catalog.DataAnnotations;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace eShop.web.Commerce.Products
{
    [CatalogContentType(GUID = "3B5D65D6-48CB-4EFA-9911-0A2C9DE1480E")]
    public class SampleProduct4 : ProductContent
    {
        //[IncludeValuesInSearchResults]
        //[IncludeInDefaultSearch]
        //[Display(Name = "Primary Image File", Order = -100)]
        //public virtual ImageFile PrimaryImage { get; set; }
    }
}