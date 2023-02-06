using EPiServer;
using EPiServer.Core;
using EPiServer.DataAbstraction;
using EPiServer.DataAnnotations;
using EPiServer.Shell.ObjectEditing;
using EPiServer.Web;
using System;
using System.ComponentModel.DataAnnotations;

namespace eShop.web.Models.Blocks
{
    [ContentType(
        DisplayName = "Big Banner Block", 
        GUID = "367c7cea-4ba5-4a0a-ad90-c40926f9b150", 
        Description = "")]
    public class BigBannerBlock : SiteBlockData
    {
        [DefaultDragAndDropTarget]
        [UIHint(UIHint.Image)]
        [Display(
            Name = "BannerImage",
            Description = "Banner Image",
            GroupName = SystemTabNames.Content,
            Order = 1)]
        public virtual Url Image { get; set; }

        [Display(
            Name = "Links Content Area",
            GroupName = SystemTabNames.Content,
            Order = 2)]
        [CultureSpecific]
        [AllowedTypes(new Type[] { typeof(BigBannerLinkBlock) })]
        public virtual ContentArea LinksContentArea { get; set; }

    }
}