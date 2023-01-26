using EPiServer.DataAbstraction;
using EPiServer.DataAnnotations;
using System.ComponentModel.DataAnnotations;

namespace eShop.web.Models.Blocks
{
    [ContentType(DisplayName = "HiddenBlock", GUID = "40dfc5db-3d43-4b7f-8f89-a315cf03d1a1", Description = "", AvailableInEditMode = false)]
    public class HiddenBlock : SiteBlockData
    {

        [CultureSpecific]
        [Display(
            Name = "Name",
            Description = "Name field's description",
            GroupName = SystemTabNames.Content,
            Order = 1)]
        public virtual string Name { get; set; }

    }
}