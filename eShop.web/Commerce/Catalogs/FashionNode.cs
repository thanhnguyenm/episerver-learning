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
        DisplayName = "Fashion Node",
        GUID = "C9EBC89F-F5F6-4DF7-8B53-1954D8293822",
        MetaClassName = "FashionNode",
        Description = "Fashion Category")]
    [AvailableContentTypes(Include = new[]
    {
        typeof(FashionProduct),
        typeof(FashionItemVariant),
        typeof(NodeContent)
    })]
    public class FashionNode : NodeContent
    {
    }
}