using EPiServer.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace eShop.web.ViewModels
{
    public class CatalogContentViewModel
    {
        public ContentReference CatalogReference { get; set; }
        public string CatalogName { get; set; }
        public string CatalogUrl { get; set; }
        public List<ProductContentViewModel> Products { get; set; }
    }
}