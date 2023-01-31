using EPiServer.Core;
using EPiServer.DataAbstraction;
using EPiServer.DataAnnotations;
using EPiServer.SpecializedProperties;
using System;
using System.ComponentModel.DataAnnotations;

namespace eShop.web.Models.Pages
{
    [ContentType(
        DisplayName = "Custom Widget Page", 
        GUID = "095d1901-980f-4672-af3c-9d7a2f86820d", 
        Description = "")]
    public class CustomWidgetPage : SitePageData
    {
        
    }
}