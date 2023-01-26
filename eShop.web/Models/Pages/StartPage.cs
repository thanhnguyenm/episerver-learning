using EPiServer.Core;
using EPiServer.DataAbstraction;
using EPiServer.DataAnnotations;
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
    public class StartPage : SitePageData
    {
        [Display(Name = "Site logo", GroupName = Global.GroupNames.SiteSettings, Order = 100)]
        public virtual SiteLogoTypeBlock SiteLogo { get; set; }

        [Display(Name = "Search page", GroupName = Global.GroupNames.SiteSettings, Order = 200)]
        public virtual PageReference SearchPageLink { get; set; }

    }
}