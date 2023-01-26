using EPiServer.DataAnnotations;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace eShop.web
{
    public class Global
    {
        /// <summary>
        /// Group names for content types and properties
        /// </summary>
        [GroupDefinitions()]
        public static class GroupNames
        {
            [Display(Name = "Contact", Order = 1)]
            public const string Contact = "Contact";

            [Display(Name = "Default", Order = 2)]
            public const string Default = "Default";

            [Display(Name = "Metadata", Order = 3)]
            public const string MetaData = "Metadata";

            [Display(Name = "News", Order = 4)]
            public const string News = "News";

            [Display(Name = "Products", Order = 5)]
            public const string Products = "Products";

            [Display(Name = "SiteSettings", Order = 6)]
            public const string SiteSettings = "SiteSettings";

            [Display(Name = "Specialized", Order = 7)]
            public const string Specialized = "Specialized";
        }

        /// <summary>
        /// Names used for UIHint attributes to map specific rendering controls to page properties
        /// </summary>
        public static class SiteUIHints
        {
            public const string Contact = "contact";
            public const string Strings = "StringList";
            public const string StringsCollection = "StringsCollection";
        }

        /// <summary>
        /// Tags to use for the main widths used in the Bootstrap HTML framework
        /// </summary>
        public static class ContentAreaTags
        {
            public const string FullWidth = "full";
            public const string TwoThirdsWidth = "wide";
            public const string HalfWidth = "half";
            public const string OneThirdWidth = "narrow";
            public const string OneFourthWidth = "quarter";
            public const string NoRenderer = "norenderer";
        }
    }
}