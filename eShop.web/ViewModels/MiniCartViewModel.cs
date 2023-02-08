using EPiServer.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace eShop.web.ViewModels
{
    public class MiniCartViewModel
    {
        public decimal ItemCount { get; set; }
        public PageReference CheckoutPage { get; set; }

        public decimal Total { get; set; }
        public decimal TotalDiscount { get; set; }

        public List<ShipmentViewModel> Shipments { get; set; }
    }
}