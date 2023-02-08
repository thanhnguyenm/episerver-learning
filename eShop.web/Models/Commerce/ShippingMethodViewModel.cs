using Mediachase.Commerce;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace eShop.web.Models.Commerce
{
    public class ShippingMethodViewModel
    {
        public Guid Id { get; set; }
        public string DisplayName { get; set; }
        public Money Price { get; set; }
    }
}