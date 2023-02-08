using EPiServer;
using EPiServer.Commerce.Order;
using EPiServer.Core;
using EPiServer.ServiceLocation;
using EPiServer.Web;
using eShop.web.Models.Pages;
using eShop.web.ViewModels;
using Mediachase.Commerce.Catalog;
using Mediachase.Commerce.Customers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace eShop.web.Controllers
{
    public class HeaderController : BasePartialController<IContentData>
    {
        [ChildActionOnly]
        public ActionResult CartMini(IContent currentContent)
        {
            var _repoRepository = ServiceLocator.Current.GetInstance<IContentRepository>();

            var startpage = SiteDefinition.Current.StartPage;
            var startpageContent = _repoRepository.Get<StartPage>(startpage);

            var _orderRepository = ServiceLocator.Current.GetInstance<IOrderRepository>();
            var _referenceConverter = ServiceLocator.Current.GetInstance<ReferenceConverter>();

            var cookie = Request.Cookies[FormsAuthentication.FormsCookieName];
            if(cookie != null)
            {
                var ticketInfo = FormsAuthentication.Decrypt(cookie.Value);
            }    

            var cart = _orderRepository.LoadCart<ICart>(CustomerContext.Current.CurrentContactId, "CartDefault");

            var model = new MiniCartViewModel() { ItemCount = 0, CheckoutPage = startpageContent.CheckoutPageLink };
            if (cart != null)
            {
                var lineitems = cart
                .GetAllLineItems()
                .Where(c => !ContentReference.IsNullOrEmpty(_referenceConverter.GetContentLink(c.Code)));

                var itemCount = lineitems.Sum(x => x.Quantity);
                model.ItemCount = itemCount;
            }
            
            return PartialView("~/Views/Cart/CartMini.cshtml", model);
        }
    }
}