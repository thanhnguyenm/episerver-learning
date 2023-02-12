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
        GUID = "550ebcfc-c989-4272-8f94-c6d079f56181",
        MetaClassName = "FashionItemVariant",
        DisplayName = "Fashion Item Variant",
        Description = "Display fashion product")]
    public class FashionItemVariant : VariationContent
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