using EPiServer.Core;
using EPiServer.DataAbstraction;
using EPiServer.DataAnnotations;
using System.ComponentModel.DataAnnotations;

namespace eShop.web.Models.Pages
{
    [ContentType(
        DisplayName = "Standard Page", 
        GUID = "ba78c496-8f87-47f0-866b-70d02dc66f21", 
        Description = "This is a simple page, using Teaser image and text")]
    public class StandardPage : SitePageData
    {
        [Display(
            Name = "Main body",
            GroupName = SystemTabNames.Content,
            Order = 310)]
        [CultureSpecific]
        public virtual XhtmlString MainBody { get; set; }

        [Display(
            Name = "Large content area",
            GroupName = SystemTabNames.Content,
            Order = 320)]
        public virtual ContentArea MainContentArea { get; set; }
    }
}