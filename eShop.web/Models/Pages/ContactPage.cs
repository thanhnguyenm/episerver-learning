using EPiServer.Core;
using EPiServer.DataAbstraction;
using EPiServer.DataAnnotations;
using EPiServer.SpecializedProperties;
using EPiServer.Web;
using System;
using System.ComponentModel.DataAnnotations;

namespace eShop.web.Models.Pages
{
    [ContentType(DisplayName = "Contact Page", 
        GUID = "e54fa03d-6a43-4bc9-9020-cea2597fc812", Description = "")]
    public class ContactPage : SitePageData
    {

        [CultureSpecific]
        [Display(
            Name = "Address title",
            Description = "The main body will be shown in the main content area of the page, using the XHTML-editor you can insert for example text, images and tables.",
            GroupName = SystemTabNames.Content,
            Order = 1)]
        public virtual string AddressTile { get; set; }

        [CultureSpecific]
        [Display(
            Name = "Address",
            Description = "The main body will be shown in the main content area of the page, using the XHTML-editor you can insert for example text, images and tables.",
            GroupName = SystemTabNames.Content,
            Order = 2)]
        public virtual string Address { get; set; }

        [CultureSpecific]
        [Display(
            Name = "Phone Title",
            Description = "The main body will be shown in the main content area of the page, using the XHTML-editor you can insert for example text, images and tables.",
            GroupName = SystemTabNames.Content,
            Order = 3)]
        public virtual string PhoneTitle { get; set; }

        [CultureSpecific]
        [Display(
            Name = "Phone",
            Description = "The main body will be shown in the main content area of the page, using the XHTML-editor you can insert for example text, images and tables.",
            GroupName = SystemTabNames.Content,
            Order = 4)]
        public virtual string Phone { get; set; }

        [CultureSpecific]
        [Display(
            Name = "Email Title",
            Description = "The main body will be shown in the main content area of the page, using the XHTML-editor you can insert for example text, images and tables.",
            GroupName = SystemTabNames.Content,
            Order = 5)]
        public virtual string EmailTitle { get; set; }

        [CultureSpecific]
        [Display(
            Name = "Email",
            Description = "The main body will be shown in the main content area of the page, using the XHTML-editor you can insert for example text, images and tables.",
            GroupName = SystemTabNames.Content,
            Order = 6)]
        public virtual string Email { get; set; }

        [CultureSpecific]
        [Display(
            Name = "Google map url",
            Description = "The main body will be shown in the main content area of the page, using the XHTML-editor you can insert for example text, images and tables.",
            GroupName = SystemTabNames.Content,
            Order = 7)]
        [UIHint(UIHint.Textarea)]
        public virtual string GoogleMapUrl { get; set; }

        [CultureSpecific]
        [Display(
            Name = "Form Title",
            Description = "The main body will be shown in the main content area of the page, using the XHTML-editor you can insert for example text, images and tables.",
            GroupName = SystemTabNames.Content,
            Order = 8)]
        public virtual string FormTitle { get; set; }

        [CultureSpecific]
        [Display(
            Name = "Name Field Tile",
            Description = "The main body will be shown in the main content area of the page, using the XHTML-editor you can insert for example text, images and tables.",
            GroupName = SystemTabNames.Content,
            Order = 9)]
        public virtual string NameFieldTile { get; set; }

        [CultureSpecific]
        [Display(
            Name = "Email Field Tile",
            Description = "The main body will be shown in the main content area of the page, using the XHTML-editor you can insert for example text, images and tables.",
            GroupName = SystemTabNames.Content,
            Order = 10)]
        public virtual string EmailFieldTile { get; set; }

        [CultureSpecific]
        [Display(
            Name = "Message Field Tile",
            Description = "The main body will be shown in the main content area of the page, using the XHTML-editor you can insert for example text, images and tables.",
            GroupName = SystemTabNames.Content,
            Order = 10)]
        public virtual string MessageFieldTile { get; set; }

        [CultureSpecific]
        [Display(
            Name = "Button Field Tile",
            Description = "The main body will be shown in the main content area of the page, using the XHTML-editor you can insert for example text, images and tables.",
            GroupName = SystemTabNames.Content,
            Order = 10)]
        public virtual string ButtonFieldTile { get; set; }


        [Display(
            Name = "Contact Form",
            Description = "The main body will be shown in the main content area of the page, using the XHTML-editor you can insert for example text, images and tables.",
            GroupName = SystemTabNames.Content,
            Order = 11)]
        public virtual ContentArea ContactFormArea { get; set; }

    }
}