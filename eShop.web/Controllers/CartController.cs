using EPiServer;
using EPiServer.Commerce.Catalog.ContentTypes;
using EPiServer.Commerce.Order;
using EPiServer.Core;
using EPiServer.ServiceLocation;
using eShop.web.Business.Services;
using eShop.web.Helpers;
using eShop.web.ViewModels;
using Mediachase.Commerce;
using Mediachase.Commerce.Catalog;
using Mediachase.Commerce.Customers;
using Mediachase.Commerce.Pricing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace eShop.web.Controllers
{
    public class CartController : Controller
    {
        private ICart _cart;
        private ICart Cart
        {
            get { return _cart ?? (_cart = _orderRepository.LoadOrCreateCart<ICart>(CustomerContext.Current.CurrentContactId, "CartDefault")); }
        }

        IOrderRepository _orderRepository;
        ReferenceConverter _referenceConverter;
        IContentLoader _contentLoader;
        IOrderGroupFactory _orderGroupFactory;
        IPriceService _priceService;
        ICurrentMarket _currentMarket;
        CookieService _cookieService;
        OrderValidationService _orderValidationService;

        public CartController()
        {
            _orderRepository = ServiceLocator.Current.GetInstance<IOrderRepository>();
            _referenceConverter = ServiceLocator.Current.GetInstance<ReferenceConverter>();
            _contentLoader = ServiceLocator.Current.GetInstance<IContentLoader>();
            _orderGroupFactory = ServiceLocator.Current.GetInstance<IOrderGroupFactory>();
            _currentMarket = ServiceLocator.Current.GetInstance<ICurrentMarket>();
            _cookieService = ServiceLocator.Current.GetInstance<CookieService>();
            _orderValidationService = ServiceLocator.Current.GetInstance<OrderValidationService>();
            _priceService = ServiceLocator.Current.GetInstance<IPriceService>();
        }

        [ChildActionOnly]
        public ActionResult CartMini()
        {
            var _orderRepository = ServiceLocator.Current.GetInstance<IOrderRepository>();
            var _referenceConverter = ServiceLocator.Current.GetInstance<ReferenceConverter>();

            var cookie = Request.Cookies[FormsAuthentication.FormsCookieName];
            if (cookie != null)
            {
                var ticketInfo = FormsAuthentication.Decrypt(cookie.Value);
            }

            var cart = _orderRepository.LoadCart<ICart>(CustomerContext.Current.CurrentContactId, "CartDefault");

            var model = new MiniCartViewModel() { ItemCount = 0 };
            if (cart != null)
            {
                var lineitems = cart
                .GetAllLineItems()
                .Where(c => !ContentReference.IsNullOrEmpty(_referenceConverter.GetContentLink(c.Code)));

                var itemCount = lineitems.Sum(x => x.Quantity);
                model.ItemCount = itemCount;
            }

            return PartialView("~/Views/Header/CartMini.cshtml", model);
        }

        [HttpPost]
        public async Task<ActionResult> AddToCart(string code)
        {
            ModelState.Clear();

            var contentLink = _referenceConverter.GetContentLink(code);
            var entryContent = _contentLoader.Get<EntryContentBase>(contentLink);

            var lineItem = Cart.GetAllLineItems().FirstOrDefault(x => x.Code == code && !x.IsGift);

            if(lineItem == null)
            {
                lineItem = Cart.CreateLineItem(code, _orderGroupFactory);

                lineItem.Quantity = 1;
                lineItem.DisplayName = entryContent.Name;
                Cart.AddLineItem(lineItem, _orderGroupFactory);

                var currency = GetCurrentCurrency();
                var price = _priceService.GetPrices(
                                            _currentMarket.GetCurrentMarket().MarketId,
                                            DateTime.Now,
                                            new CatalogKey(code),
                                            new PriceFilter { Currencies = new[] { currency } })
                                            .OrderBy(x => x.UnitPrice.Amount).FirstOrDefault();


                if (price != null)
                {
                    lineItem.PlacedPrice = price.UnitPrice.Amount;
                }
            }
            else
            {
                var shipment = Cart.GetFirstShipment();
                Cart.UpdateLineItemQuantity(shipment, lineItem, lineItem.Quantity + 1);
            }
            var validationIssues = ValidateCart(Cart);

            if(!validationIssues.Any() || !validationIssues.HasItemBeenRemoved(lineItem))
            {
                _orderRepository.Save(Cart);

                //var change = new CartChangeData(CartChangeType.ItemAdded, code);
                //await _recommendationService.TrackCartAsync(HttpContext, new List<CartChangeData> { change });
                return CartMini();
            }    



            return new HttpStatusCodeResult(500, "Cannot add item to cart");
        }

        public IDictionary<ILineItem, IList<ValidationIssue>> ValidateCart(ICart cart)
        {
            return _orderValidationService.ValidateOrder(cart);
        }

        public IEnumerable<Currency> GetAvailableCurrencies()
        {
            return _currentMarket.GetCurrentMarket().Currencies;
        }

        public virtual Currency GetCurrentCurrency()
        {
            Currency currency;
            return TryGetCurrency(_cookieService.Get("Currency"), out currency) ?
                currency :
                _currentMarket.GetCurrentMarket().DefaultCurrency;
        }

        private bool TryGetCurrency(string currencyCode, out Currency currency)
        {
            var result = GetAvailableCurrencies()
                .Where(x => x.CurrencyCode == currencyCode)
                .Cast<Currency?>()
                .FirstOrDefault();

            if (result.HasValue)
            {
                currency = result.Value;
                return true;
            }

            currency = null;
            return false;
        }
    }
}