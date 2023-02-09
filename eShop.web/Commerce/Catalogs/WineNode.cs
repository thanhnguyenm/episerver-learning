using EPiServer.Commerce.Catalog.ContentTypes;
using EPiServer.Commerce.Catalog.DataAnnotations;
using EPiServer.DataAnnotations;
using eShop.web.Commerce.Products;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace eShop.web.Commerce.Catalogs
{
    [CatalogContentType(
        DisplayName = "Wine Node",
        GUID = "03822AFE-BAE6-4C4C-A4A8-C72290DF85AA",
        MetaClassName = "WineNode")]
    [AvailableContentTypes(Include = new[]
    {
        typeof(WineProduct),
        typeof(NodeContent)
    })]
    public class WineNode : NodeContent
    {

    }
}