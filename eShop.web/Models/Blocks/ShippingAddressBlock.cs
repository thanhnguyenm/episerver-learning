using EPiServer.Core;
using EPiServer.DataAbstraction;
using EPiServer.DataAnnotations;
using System;
using System.ComponentModel.DataAnnotations;

namespace eShop.web.Models.Blocks
{
    [SiteContentType(
        DisplayName = "Shipping Address Block", 
        GUID = "5e38b2b9-daaf-44ad-bbf3-dbcca1f54703", 
        Description = "")]
    [BlockSiteImageUrl]
    public class ShippingAddressBlock : SiteBlockData
    {
        public ShippingAddressBlock()
        {
            Address = new ShippingAddress();
        }

        [Ignore]
        public ShippingAddress Address { get; set; }

        [Display(
            Name = "Heading",
            GroupName = SystemTabNames.Content,
            Order = 100)]
        [Required]
        public virtual string Heading { get; set; }

        [Display(
            Name = "Address Line 1 Label",
            GroupName = SystemTabNames.Content,
            Order = 200)]
        [Required]
        public virtual string Address1Text { get; set; }

        [Display(
            Name = "Address Line 2 Label",
            GroupName = SystemTabNames.Content,
            Order = 300)]
        [Required]
        public virtual string Address2Text { get; set; }

        [Display(
            Name = "Town Label",
            GroupName = SystemTabNames.Content,
            Order = 400)]
        [Required]
        public virtual string TownText { get; set; }

        [Display(
            Name = "Postcode Label",
            GroupName = SystemTabNames.Content,
            Order = 500)]
        [Required]
        public virtual string PostcodeText { get; set; }

    }
}