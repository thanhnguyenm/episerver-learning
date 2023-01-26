﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace eShop.web.Models.Blocks
{
    public class ShippingAddress
    {
        public string AddressLine1 { get; set; }

        public string AddressLine2 { get; set; }

        public string Town { get; set; }

        public string Postcode { get; set; }
    }
}