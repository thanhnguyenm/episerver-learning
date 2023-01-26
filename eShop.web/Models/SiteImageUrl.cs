using EPiServer.DataAnnotations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace eShop.web.Models
{
    /// <summary>
    /// Attribute to set the default thumbnail for site page and block types
    /// </summary>
    public class PageSiteImageUrl : ImageUrlAttribute
    {
        /// <summary>
        /// The parameterless constructor will initialize a SiteImageUrl attribute with a default thumbnail
        /// </summary>
        public PageSiteImageUrl() : base("~/Static/gfx/page-type-icon.png")
        {

        }

        public PageSiteImageUrl(string path) : base(path)
        {

        }
    }

    /// <summary>
    /// Attribute to set the default thumbnail for site page and block types
    /// </summary>
    public class BlockSiteImageUrl : ImageUrlAttribute
    {
        /// <summary>
        /// The parameterless constructor will initialize a SiteImageUrl attribute with a default thumbnail
        /// </summary>
        public BlockSiteImageUrl() : base("~/Static/gfx/block-type-icon.png")
        {

        }

        public BlockSiteImageUrl(string path) : base(path)
        {

        }
    }
}