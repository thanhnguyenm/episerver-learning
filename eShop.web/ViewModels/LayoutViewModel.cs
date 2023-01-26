using eShop.web.Models.Blocks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace eShop.web.ViewModels
{
    public class LayoutViewModel
    {
        public SiteLogoTypeBlock SiteLogo { get; set; }

        public bool HideHeader { get; set; }
        public bool HideFooter { get; set; }
    }
}