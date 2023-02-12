using EPiServer;
using EPiServer.Commerce.Catalog.ContentTypes;
using EPiServer.Commerce.Catalog.Linking;
using EPiServer.Commerce.Order;
using EPiServer.Core;
using EPiServer.Framework.DataAnnotations;
using EPiServer.Framework.Localization;
using EPiServer.ServiceLocation;
using EPiServer.Web;
using EPiServer.Web.Mvc;
using EPiServer.Web.Mvc.Html;
using EPiServer.Web.Routing;
using eShop.web.Business.Services;
using eShop.web.Helpers;
using eShop.web.Models.Commerce;
using eShop.web.Models.Pages;
using eShop.web.ViewModels;
using Mediachase.Commerce;
using Mediachase.Commerce.Catalog;
using Mediachase.Commerce.Customers;
using Mediachase.Commerce.Markets;
using Mediachase.Commerce.Orders;
using Mediachase.Commerce.Orders.Managers;
using Mediachase.Commerce.Orders.Search;
using Mediachase.Commerce.Pricing;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web.Mvc;

namespace eShop.web.Controllers
{
    [Authorize(Roles = "WebEditors, WedAdmins, Administrators")]
    public class CustomWidgetPageController : BasePageController<CustomWidgetPage>
    {
        IOrderRepository _orderRepository;
        ReferenceConverter _referenceConverter;
        IContentLoader _contentLoader;
        IOrderGroupFactory _orderGroupFactory;
        IPriceService _priceService;
        ICurrentMarket _currentMarket;
        CookieService _cookieService;
        OrderValidationService _orderValidationService;
        IMarketService _marketService;
        IShippingPlugin _shippingPlugins;
        IShippingGateway _shippingGateways;
        UrlResolver _urlResolver;
        IRelationRepository _relationRepository;
        IOrderGroupCalculator _orderGroupCalculator;
        IPaymentProcessor _paymentProcessor;
        LocalizationService _localizationService;

        public CustomWidgetPageController()
        {
            _orderRepository = ServiceLocator.Current.GetInstance<IOrderRepository>();
            _referenceConverter = ServiceLocator.Current.GetInstance<ReferenceConverter>();
            _contentLoader = ServiceLocator.Current.GetInstance<IContentLoader>();
            _orderGroupFactory = ServiceLocator.Current.GetInstance<IOrderGroupFactory>();
            _currentMarket = ServiceLocator.Current.GetInstance<ICurrentMarket>();
            _cookieService = ServiceLocator.Current.GetInstance<CookieService>();
            _orderValidationService = ServiceLocator.Current.GetInstance<OrderValidationService>();
            _priceService = ServiceLocator.Current.GetInstance<IPriceService>();
            _marketService = ServiceLocator.Current.GetInstance<IMarketService>();
            _shippingPlugins = ServiceLocator.Current.GetInstance<IShippingPlugin>();
            _shippingGateways = ServiceLocator.Current.GetInstance<IShippingGateway>();
            _relationRepository = ServiceLocator.Current.GetInstance<IRelationRepository>();
            _urlResolver = ServiceLocator.Current.GetInstance<UrlResolver>();
            _orderGroupCalculator = ServiceLocator.Current.GetInstance<IOrderGroupCalculator>();
            _paymentProcessor = ServiceLocator.Current.GetInstance<IPaymentProcessor>();
            _localizationService = ServiceLocator.Current.GetInstance<LocalizationService>();
        }

        public ActionResult Index(CustomWidgetPage currentPage)
        {
            /* Implementation of action. You can create your own view model class that you pass to the view or
             * you can pass the page type for simpler templates */
            var viewmodel = new CustomWidgetPageViewModel(currentPage);

            int startIndex = 0;
            int totalRecords = 0;
            int daysOld = 7;
            OrderSearchParameters parameters = new OrderSearchParameters();
            parameters.SqlMetaWhereClause = String.Format("Meta.Modified < GETDATE() - {0}", daysOld.ToString());
            OrderSearchOptions options = new OrderSearchOptions();
            options.StartingRecord = startIndex;
            OrderContext.Current.Search<PurchaseOrder>(parameters, options, out totalRecords);
            if (totalRecords > 0)
            {
                var pos = OrderContext.Current.Search<PurchaseOrder>(parameters, options, out totalRecords);

                viewmodel.NewOrders = pos.ToList();
            }

            return PartialView("Widgets/_CustomWidgetPage", viewmodel);
            //return View("Index", viewmodel);
        }
    }
}