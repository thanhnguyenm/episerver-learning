using EPiServer.Commerce.Catalog.ContentTypes;
using EPiServer.Commerce.Catalog.DataAnnotations;
using EPiServer.DataAnnotations;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace eShop.web.Commerce.Products
{
    [CatalogContentType(
        DisplayName = "FashionProduct",
        GUID = "20BE5F63-7B00-46DA-AFF9-E2DC1E791989")]
    public class FashionProduct : ProductContent
    {
        [CultureSpecific]
        [Tokenize]
        [Encrypted]
        [UseInComparison]
        [IncludeValuesInSearchResults]
        [IncludeInDefaultSearch]
        [SortableInSearchResults]
        [Searchable]
        [Display(Name = "ProductName", Order =-15)]
        public virtual string ProductName { get; set; }
    }
}