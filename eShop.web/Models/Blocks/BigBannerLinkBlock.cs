using EPiServer;
using EPiServer.Core;
using EPiServer.DataAbstraction;
using EPiServer.DataAnnotations;
using System;
using System.ComponentModel.DataAnnotations;

namespace eShop.web.Models.Blocks
{
    [ContentType(
        DisplayName = "Big Banner Link Block", 
        GUID = "5e3f57d7-341d-4dde-8043-24657eb70cdd", 
        Description = "")]
    public class BigBannerLinkBlock : SiteBlockData
    {
        [CultureSpecific]
        [Display(
            Name = "Sub Title",
            Description = "Sub Title",
            GroupName = SystemTabNames.Content,
            Order = 1)]
        public virtual string SubTitle { get; set; }

        [CultureSpecific]
        [Display(
            Name = "Main Title",
            Description = "Main Title",
            GroupName = SystemTabNames.Content,
            Order = 2)]
        public virtual string MainTitle { get; set; }

        [CultureSpecific]
        [Display(
            Name = "Link To Page",
            Description = "Link To Page",
            GroupName = SystemTabNames.Content,
            Order = 3)]
        public virtual Url Url { get; set; }

        [CultureSpecific]
        [Display(
            Name = "Link Text",
            Description = "Link Text",
            GroupName = SystemTabNames.Content,
            Order = 4)]
        public virtual string LinkText { get; set; }

    }
}