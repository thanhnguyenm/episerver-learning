using EPiServer.Core;
using EPiServer.DataAbstraction;
using EPiServer.DataAnnotations;
using EPiServer.Web;
using eShop.web.Business.Rendering;
using eShop.web.Models.Properties;
using System.ComponentModel.DataAnnotations;
using static eShop.web.Global;

namespace eShop.web.Models.Pages
{
    public class SitePageData : PageData, ICustomCssInContentArea
    {
        // -- Metadata
        [Display(
            Name = "Page Title",
            GroupName = GroupNames.MetaData,
            Description = "Page Title on the browser",
            Order = 100)]
        [CultureSpecific]
        public virtual string MetaTitle
        {
            get
            {
                var metaTitle = this.GetPropertyValue(p => p.MetaTitle);

                // Use explicitly set meta title, otherwise fall back to page name
                return !string.IsNullOrWhiteSpace(metaTitle)
                        ? metaTitle
                        : PageName;
            }
            set { this.SetPropertyValue(p => p.MetaTitle, value); }
        }

        [Display(
            Name = "Page keywords",
            GroupName = Global.GroupNames.MetaData,
            Order = 200)]
        [CultureSpecific]
        [BackingType(typeof(PropertyStringList))]
        public virtual string[] MetaKeywords { get; set; }

        [Display(
            Name = "Page description",
            GroupName = Global.GroupNames.MetaData,
            Order = 300)]
        [CultureSpecific]
        [UIHint(UIHint.Textarea)]
        public virtual string MetaDescription { get; set; }

        // -- Content
        [Display(
            Name = "Teaser image",
            GroupName = SystemTabNames.Content,
            Order = 100)]
        [UIHint(UIHint.Image)]
        public virtual ContentReference PageImage { get; set; }

        [Display(
            Name = "Teaser text",
            GroupName = SystemTabNames.Content,
            Order = 200)]
        [CultureSpecific]
        [UIHint(UIHint.Textarea)]
        public virtual string TeaserText
        {
            get
            {
                var teaserText = this.GetPropertyValue(p => p.TeaserText);

                // Use explicitly set teaser text, otherwise fall back to description
                return !string.IsNullOrWhiteSpace(teaserText)
                        ? teaserText
                        : MetaDescription;
            }
            set { this.SetPropertyValue(p => p.TeaserText, value); }
        }

        // -- Setting
        [Display(
            Name = "Hide Site Header",
            GroupName = SystemTabNames.Settings,
            Order = 200)]
        [CultureSpecific]
        public virtual bool HideSiteHeader { get; set; }

        [Display(
            Name = "Hide Site Footer",
            GroupName = SystemTabNames.Settings,
            Order = 300)]
        [CultureSpecific]
        public virtual bool HideSiteFooter { get; set; }

        public string ContentAreaCssClass
        {
            get { return ""; } //Page partials should be style like teasers
        }
    }
}