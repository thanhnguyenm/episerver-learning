using EPiServer.Commerce.Catalog.ContentTypes;
using EPiServer.Commerce.Catalog.DataAnnotations;
using EPiServer.Core;
using EPiServer.DataAnnotations;
using EPiServer.Shell.ObjectEditing;
using EPiServer.SpecializedProperties;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace eShop.web.Commerce.Products
{
    [CatalogContentType(
        DisplayName = "Fashion Product",
        GUID = "20BE5F63-7B00-46DA-AFF9-E2DC1E791989",
        MetaClassName = nameof(FashionProduct),
        Description = "Display fashion product")]
    public class FashionProduct : ProductContent
    {
        //[CultureSpecific]
        //[Tokenize]
        //[Encrypted]
        //[UseInComparison]
        //[IncludeValuesInSearchResults]
        //[IncludeInDefaultSearch]
        //[SortableInSearchResults]
        //[Searchable]
        //[Display(Name = "Product Name", Order = -100)]
        //public virtual string ProductName { get; set; }

        [CultureSpecific]
        [Tokenize]
        [Encrypted]
        [UseInComparison]
        [IncludeValuesInSearchResults]
        [IncludeInDefaultSearch]
        [SortableInSearchResults]
        [Searchable]
        [Display(Name = "Material", Order = -90)]
        public virtual string Material { get; set; }

        [BackingType(typeof(PropertyIntegerList))]
        [Display(Name = "Sizes", Order = -80)]
        public virtual IList<int> Sizes { get; set; }

        [CultureSpecific]
        [Tokenize]
        [Encrypted]
        [UseInComparison]
        [IncludeValuesInSearchResults]
        [IncludeInDefaultSearch]
        [Searchable]
        [Display(Name = "Product Specification", Order = -70)]
        public virtual XhtmlString ProductSpecification { get; set; }

        
    }
}