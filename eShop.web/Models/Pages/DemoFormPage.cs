using EPiServer.Core;
using EPiServer.DataAbstraction;
using EPiServer.DataAnnotations;
using System.ComponentModel.DataAnnotations;
using static eShop.web.Global;

namespace eShop.web.Models.Pages
{
    [SiteContentType(DisplayName = "DemoFormPage", 
        GUID = "0d458e59-5391-4a82-aa7a-db35ac9aa6d9", 
        Description = "",
        GroupName = GroupNames.Specialized)]
    [PageSiteImageUrl]
    public class DemoFormPage : SitePageData
    {

        [CultureSpecific]
        [Display(
            Name = "Main body",
            GroupName = SystemTabNames.Content,
            Order = 310)]
        public virtual ContentArea MainContentArea { get; set; }

    }
}