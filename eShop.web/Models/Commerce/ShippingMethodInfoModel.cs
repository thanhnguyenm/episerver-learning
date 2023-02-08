using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace eShop.web.Models.Commerce
{
    public class ShippingMethodInfoModel
    {
        public Guid MethodId { get; set; }
        public string ClassName { get; set; }
        public string LanguageId { get; set; }
        public string Currency { get; set; }
        public int Ordering { get; set; }
    }
}