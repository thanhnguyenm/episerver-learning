using EPiServer.Core;
using EPiServer.DataAbstraction;
using EPiServer.DataAnnotations;
using System;
using System.ComponentModel.DataAnnotations;

namespace eShop.web.Models.Blocks
{
    [SiteContentType(
        DisplayName = "Hot Trend Block", 
        GUID = "843c3af3-4c78-4d6c-9051-e8614312ce79", 
        Description = "")]
    [BlockSiteImageUrl]
    public class HotTrendBlock : SiteBlockData
    {
        /*
                [CultureSpecific]
                [Display(
                    Name = "Name",
                    Description = "Name field's description",
                    GroupName = SystemTabNames.Content,
                    Order = 1)]
                public virtual string Name { get; set; }
         */
    }
}