using EPiServer.Core;
using EPiServer.DataAbstraction;
using EPiServer.DataAnnotations;
using EPiServer.SpecializedProperties;
using System;
using System.ComponentModel.DataAnnotations;

namespace eShop.web.Models.Pages
{
    [ContentType(DisplayName = "Blog Category Page", GUID = "3468750f-1d58-4706-9d95-a0b08bc2e9d6", Description = "")]
    public class BlogCategoryPage : PageData
    {

        [CultureSpecific]
        [Display(
            Name = "Category Name",
            Description = "Category Name",
            GroupName = SystemTabNames.Content,
            Order = 100)]
        public virtual string CategoryName { get; set; }

    }
}