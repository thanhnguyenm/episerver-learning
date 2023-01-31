using EPiServer;
using EPiServer.DataAbstraction;
using EPiServer.DataAnnotations;
using EPiServer.Shell.ObjectEditing;
using EPiServer.Web;
using eShop.web.Business.Rendering;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace eShop.web.Models.Blocks
{
    [ContentType(
        DisplayName = "Category Banner Block", 
        GUID = "c29ce423-0f24-441c-973b-0fbb593a02d0", 
        Description = "The category banner on Start page")]
    [BlockSiteImageUrl]
    public class CategoryBannerBlock : SiteBlockData, IDefaultDisplayOption, IDisallowDisplayOption
    {

        [CultureSpecific]
        [Display(
            Name = "Title",
            Description = "Category banner title",
            GroupName = SystemTabNames.Content,
            Order = 1)]
        [Required]
        public virtual string Title { get; set; }

        [CultureSpecific]
        [Display(
            Name = "Description",
            Description = "Category banner description",
            GroupName = SystemTabNames.Content,
            Order = 2)]
        [Required]
        public virtual string Description { get; set; }

        [CultureSpecific]
        [Display(
            Name = "Link To Page",
            Description = "Link to page",
            GroupName = SystemTabNames.Content,
            Order = 3)]
        public virtual Url Link { get; set; }

        /// <summary>
        /// Gets the site logotype URL
        /// </summary>
        /// <remarks>If not specified a default logotype will be used</remarks>
        [CultureSpecific]
        [DefaultDragAndDropTarget]
        [UIHint(UIHint.Image)]
        [Required]
        [Display(
            Name = "Background image",
            Description = "Background Image",
            GroupName = SystemTabNames.Content,
            Order = 4)]
        public virtual Url Url { get; set; }

        public string DefaultDisplayOption => Global.ContentAreaTags.HalfWidth;

        public IEnumerable<string> DisabledDisplayOptions => new List<string> { Global.ContentAreaTags.OneFourthWidth, Global.ContentAreaTags.OneThirdWidth };
    }
}