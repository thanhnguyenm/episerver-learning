using eShop.web.Models.Pages;
using Mediachase.Commerce.Orders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace eShop.web.ViewModels
{
    public class CustomWidgetPageViewModel : PageViewModel<CustomWidgetPage>
    {
        public CustomWidgetPageViewModel(CustomWidgetPage currentPage) : base(currentPage)
        {

        }

        public List<PurchaseOrder> NewOrders { get; set; }
    }
}