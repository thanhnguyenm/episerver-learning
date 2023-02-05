using EPiServer.Core;
using EPiServer.DataAbstraction;
using EPiServer.DataAnnotations;
using System;
using System.ComponentModel.DataAnnotations;

namespace eShop.web.Models.Blocks
{
    [ContentType(
        DisplayName = "NewProductBlock", 
        GUID = "55949ceb-f38a-4899-9986-efdea8fc9ac0", 
        Description = "Display New Products")]
    public class NewProductBlock : SiteBlockData
    {
        [CultureSpecific]
        [Display(
            Name = "Heading",
            GroupName = SystemTabNames.Content,
            Order = 100)]
        [Required]
        public virtual string Heading { get; set; }

        [Display(
            Name = "Number Of Products",
            GroupName = SystemTabNames.Content,
            Order = 101)]
        [Required]
        public virtual string NumberOfProducts { get; set; }
    }
}