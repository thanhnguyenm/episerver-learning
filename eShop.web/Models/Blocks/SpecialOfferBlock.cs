using EPiServer.Core;
using EPiServer.DataAbstraction;
using EPiServer.DataAnnotations;
using System;
using System.ComponentModel.DataAnnotations;

namespace eShop.web.Models.Blocks
{
    [ContentType(DisplayName = "Special Offer Block", 
        GUID = "39127041-ada4-4ffa-8ead-6d045f8406ff", 
        Description = "")]
    public class SpecialOfferBlock : SiteBlockData
    {

        [CultureSpecific]
        [Display(
            Name = "Title",
            Description = "Title",
            GroupName = SystemTabNames.Content,
            Order = 1)]
        public virtual string Title { get; set; }

        [CultureSpecific]
        [Display(
            Name = "Is Package",
            Description = "Is Package",
            GroupName = SystemTabNames.Content,
            Order = 2)]
        public virtual bool IsPackage { get; set; }

    }
}