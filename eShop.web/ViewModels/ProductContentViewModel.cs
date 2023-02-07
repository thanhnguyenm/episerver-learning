using eShop.web.Models.Media;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace eShop.web.ViewModels
{
    public class ProductContentViewModel
    {
        public string Code { get; set; }
        public string ProductName { get; set; }
        public decimal MinPrice { get; set; }
        public decimal MaxPrice { get; set; }
        public string Image { get; set; }
        public string ProductURL { get; set; }
        public List<string> DetailImages { get; set; }

        public List<ProductContentViewModel> Variants { get; set; } 

        // SEO
        public string Keywords { get; set; }
        public string Description { get; set; }
    }
}