using EPiServer.Core;
using EPiServer.DataAbstraction;
using EPiServer.DataAnnotations;
using EPiServer.SpecializedProperties;
using System;
using System.ComponentModel.DataAnnotations;

namespace eShop.web.Models.Pages
{
    [SiteContentType(DisplayName = "ContainerPage", 
        GUID = "ac62bb42-be42-4028-9efe-d8f6246c280a",
        GroupName = Global.GroupNames.Specialized,
        Description = "")]
    [PageSiteImageUrl]
    public class ContainerPage : SitePageData, IContainerPage
    {
        
    }
}