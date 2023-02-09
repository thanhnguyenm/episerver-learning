using EPiServer.Commerce.Order;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace eShop.web.ViewModels
{
    public class PaymentMethodViewModel
    {
        //public IPaymentMethod PaymentMethod { get; set; }


        public Guid PaymentMethodId { get; set; }
        public string SystemKeyword { get; set; }
        public string PaymentMethodName { get; set; }
        public bool IsDefault { get; set; }
    }
}