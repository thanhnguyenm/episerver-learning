using EPiServer.Commerce.Catalog.ContentTypes;
using EPiServer.Commerce.Catalog.DataAnnotations;
using EPiServer.DataAnnotations;
using EPiServer.Shell.ObjectEditing;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace eShop.web.Commerce.Products
{
    [CatalogContentType(
        GUID = "B1D9F167-B900-412E-8CB8-516EFFEFB912",
        MetaClassName = "FashionProductVariation",
        DisplayName = "Fashion Product Variation",
        Description = "Display fashion product variation")]
    public class FashionProductVariation : VariationContent
    {
        [Searchable]
        [IncludeInDefaultSearch]
        [Display(Name = "Size", Order = 1)]
        public virtual int Size { get; set; }

        [Searchable]
        [Tokenize]
        [IncludeInDefaultSearch]
        [Display(Name = "Colour", Order = 2)]
        [ClientEditor(ClientEditingClass = "dijit/ColorPalette")]
        public virtual string Colour { get; set; }
    }
}