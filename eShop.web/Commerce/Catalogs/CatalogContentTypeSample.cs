using EPiServer.Commerce.Catalog.ContentTypes;
using EPiServer.Commerce.Catalog.DataAnnotations;
using EPiServer.DataAnnotations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace eShop.web.Commerce.Catalogs
{
    [CatalogContentType(GUID = "1FD3E34D-6476-40E2-8CB5-8EFB0DB79E3E")]
    public class SampleNode : NodeContent
    {
        [CultureSpecific]
        [Tokenize]
        [Encrypted]
        [UseInComparison]
        [IncludeValuesInSearchResults]
        [IncludeInDefaultSearch]
        [SortableInSearchResults]
        public virtual string Description { get; set; }

    }
}