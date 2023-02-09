using EPiServer.Core;
using EPiServer.DataAbstraction;
using EPiServer.DataAnnotations;
using EPiServer.Shell.ObjectEditing;
using EPiServer.SpecializedProperties;
using eShop.web.Business.SelectionFactories;
using System;
using System.ComponentModel.DataAnnotations;

namespace eShop.web.Models.Pages
{
    [ContentType(DisplayName = "Blog Page", 
        GUID = "94ebfbcd-b0f2-4fb4-9025-24a1f9264b6c", Description = "")]
    public class BlogPage : SitePageData
    {

        [CultureSpecific]
        [Display(
            Name = "Blog Category",
            Description = "Choose a blog category",
            GroupName = SystemTabNames.Content,
            Order = 300)]
        [SelectOne(SelectionFactoryType = typeof(CategoriesFactory))]
        public virtual string BlogCategory { get; set; }

    }
}