using eShop.web.Models.Pages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace eShop.web.ViewModels
{
    public class PurchaseOrderViewModel : PageViewModel<CheckoutPage>
    {
        public PurchaseOrderViewModel(CheckoutPage currentPage) : base(currentPage)
        {
        }

        public string OrderNumber { get; set; }
    }
}