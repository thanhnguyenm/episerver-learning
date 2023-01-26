using EPiServer.DataAbstraction;
using EPiServer.DataAnnotations;
using System.ComponentModel.DataAnnotations;

namespace eShop.web.Models.Blocks
{
    [ContentType(
        DisplayName = "Colour Block", 
        GUID = "801ff44e-144e-42b4-bb9e-14edabdbf742", 
        Description = "")]
    [BlockSiteImageUrl]
    public class ColourBlock : SiteBlockData
    {

        [CultureSpecific]
        [Display(
            Name = "Name",
            Description = "Name field's description",
            GroupName = SystemTabNames.Content,
            Order = 1)]
        public virtual string Name { get; set; }

        [CultureSpecific]
        [Display(
            Name = "Colour",
            Description = "Colour",
            GroupName = SystemTabNames.Content,
            Order = 2)]
        public virtual string Colour { get; set; }

    }
}