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
    [ContentType(DisplayName = "Service Block", 
        GUID = "9461f98d-b165-4c9d-9d15-70efc544ffcb", 
        Description = "")]
    public class ServiceBlock : SiteBlockData
    {

        [CultureSpecific]
        [Required]
        [Display(
            Name = "Service Name",
            Description = "Service Name",
            GroupName = SystemTabNames.Content,
            Order = 1)]
        public virtual string ServiceName { get; set; }

        [CultureSpecific]
        [Required]
        [Display(
            Name = "Descripttion",
            Description = "Descripttion",
            GroupName = SystemTabNames.Content,
            Order = 2)]
        public virtual string Descripttion { get; set; }

        [CultureSpecific]
        [Required]
        [Display(
            Name = "Icon Css Class",
            Description = "Icon Css Class",
            GroupName = SystemTabNames.Content,
            Order = 3)]
        public virtual string IconClass { get; set; }

    }
}