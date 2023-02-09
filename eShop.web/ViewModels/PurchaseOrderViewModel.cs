using eShop.web.Models.Pages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace eShop.web.ViewModels
{
    public class PurchaseOrderViewModel : PageViewModel<StartPage>
    {
        public PurchaseOrderViewModel(StartPage currentPage) : base(currentPage)
        {
        }

        public string OrderNumber { get; set; }
    }
}