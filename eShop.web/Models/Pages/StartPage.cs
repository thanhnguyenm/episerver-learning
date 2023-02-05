using EPiServer.Core;
using EPiServer.DataAbstraction;
using EPiServer.DataAnnotations;
using eShop.web.Business.Filters;
using eShop.web.Models.Blocks;
using System.ComponentModel.DataAnnotations;
using static eShop.web.Global;

namespace eShop.web.Models.Pages
{
    /// <summary>
    /// Used for the site's start page and also acts as a container for site settings
    /// </summary>
    [ContentType(
        DisplayName = "Start Page",
        GUID = "cc25cce1-656a-43b8-b6e7-e74ad5e9326b",
        Description = "The template used for homepage for a site",
        GroupName = GroupNames.Specialized)]
    [PageSiteImageUrl]
    //[AvailableContentTypes(
    //    Availability.Specific,
    //    Include = new[] { typeof(ContainerPage), typeof(ProductPage), typeof(StandardPage), typeof(ISearchPage), typeof(LandingPage), typeof(ContentFolder) }, // Pages we can create under the start page...
    //    ExcludeOn = new[] { typeof(ContainerPage), typeof(ProductPage), typeof(StandardPage), typeof(ISearchPage), typeof(LandingPage) })] // ...and underneath those we can't create additional start pages
    public class StartPage : SitePageData
    {
        // -- Site Setting
        [Display(Name = "Site logo", GroupName = Global.GroupNames.SiteSettings, Order = 100)]
        public virtual SiteLogoTypeBlock SiteLogo { get; set; }

        [Display(Name = "Search page", GroupName = Global.GroupNames.SiteSettings, Order = 200)]
        public virtual PageReference SearchPageLink { get; set; }

        // --Content

        [Display(
            Name = "Example String",
            GroupName = SystemTabNames.Content,
            Order = 320)]
        [UIHint("ExampleString")]
        public virtual string ExampleString { get; set; }

        [Display(
            Name = "Large Content Area",
            GroupName = SystemTabNames.Content,
            Order = 320)]
        [CultureSpecific]
        public virtual ContentArea MainContentArea { get; set; }

        [Display(
            Name = "Main Category Banner",
            GroupName = SystemTabNames.Content,
            Order = 330)]
        public virtual CategoryBannerBlock MainCategoryBanner { get; set; }

        [Display(
            Name = "Small Banner Content Area",
            GroupName = SystemTabNames.Content,
            Order = 331)]
        [CultureSpecific]
        //[AllowedTypes(new[] { typeof(CategoryBannerBlock) })]
        //[AvailableContentTypes]
        [MaxItemsAllowedAtribute(4)]
        [DisableInvalidDisplayOptionsAttribute]
        public virtual ContentArea SmallCategoryBannerContentArea { get; set; }
    }
}