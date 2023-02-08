using EPiServer;
using EPiServer.Commerce.Catalog.ContentTypes;
using EPiServer.Commerce.Catalog.Linking;
using EPiServer.Commerce.Order;
using EPiServer.Commerce.Shell;
using EPiServer.Core;
using EPiServer.Globalization;
using EPiServer.ServiceLocation;
using EPiServer.Web.Routing;
using eShop.web.Business.Services;
using eShop.web.Helpers;
using eShop.web.Models.Commerce;
using eShop.web.ViewModels;
using Mediachase.Commerce;
using Mediachase.Commerce.Catalog;
using Mediachase.Commerce.Customers;
using Mediachase.Commerce.Markets;
using Mediachase.Commerce.Orders;
using Mediachase.Commerce.Orders.Managers;
using Mediachase.Commerce.Pricing;
using System;
using System.Collections.Generic;
using System.Globalization;
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
        IOrderGroupCalculator _orderGroupCalculator;
        IMarketService _marketService;
        IShippingPlugin _shippingPlugins;
        IShippingGateway _shippingGateways;
        CatalogContentService _catalogContentService;
        LanguageResolver _languageResolver;
        UrlResolver _urlResolver;
        IRelationRepository _relationRepository;

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
            _marketService = ServiceLocator.Current.GetInstance<IMarketService>();
            _shippingPlugins = ServiceLocator.Current.GetInstance<IShippingPlugin>();
            _shippingGateways = ServiceLocator.Current.GetInstance<IShippingGateway>();
            _catalogContentService = ServiceLocator.Current.GetInstance<CatalogContentService>();
            _languageResolver = ServiceLocator.Current.GetInstance<LanguageResolver>();
            _urlResolver = ServiceLocator.Current.GetInstance<UrlResolver>();
            _relationRepository = ServiceLocator.Current.GetInstance<IRelationRepository>();
            _orderGroupCalculator = ServiceLocator.Current.GetInstance<IOrderGroupCalculator>();
        }

        [ChildActionOnly]
        [OutputCache(Duration = 0, NoStore = true)]
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

            return PartialView("CartMini", model);
        }

        [ChildActionOnly]
        public ActionResult CartSideBar()
        {
            var _orderRepository = ServiceLocator.Current.GetInstance<IOrderRepository>();
            var _referenceConverter = ServiceLocator.Current.GetInstance<ReferenceConverter>();

            var cookie = Request.Cookies[FormsAuthentication.FormsCookieName];
            if (cookie != null)
            {
                var ticketInfo = FormsAuthentication.Decrypt(cookie.Value);
            }

            var cart = _orderRepository.LoadCart<ICart>(CustomerContext.Current.CurrentContactId, "CartDefault");

            var model = new MiniCartViewModel() { ItemCount = 0, Total = 0, TotalDiscount = 0, Shipments = new List<ShipmentViewModel>() };
            if (cart != null)
            {
                var lineitems = cart
                .GetAllLineItems()
                .Where(c => !ContentReference.IsNullOrEmpty(_referenceConverter.GetContentLink(c.Code)));

                var itemCount = lineitems.Sum(x => x.Quantity);
                model.ItemCount = itemCount;
                model.Total = _orderGroupCalculator.GetSubTotal(Cart);

                foreach (var shipment in cart.GetFirstForm().Shipments)
                {
                    var shipmentModel = new ShipmentViewModel
                    {
                        ShipmentId = shipment.ShipmentId,
                        CartItems = new List<CartItemViewModel>(),
                        Address = ConvertToModel(shipment.ShippingAddress),
                        ShippingMethods = CreateShippingMethodViewModels(cart.MarketId, cart.Currency, shipment)
                    };

                    shipmentModel.ShippingMethodId = shipment.ShippingMethodId == Guid.Empty && shipmentModel.ShippingMethods.Any() ?
                                                 shipmentModel.ShippingMethods.First().Id
                                               : shipment.ShippingMethodId;

                    var codes = shipment.LineItems.Select(x => x.Code);
                    var entries = _contentLoader.GetItems(
                                    codes
                                        .Select(x => _referenceConverter.GetContentLink(x))
                                        .Where(r => !ContentReference.IsNullOrEmpty(r)),
                                    //_languageResolver.GetPreferredCulture()
                                    GetCurrentLanguage()
                                    ).OfType<EntryContentBase>();

                    foreach (var lineItem in shipment.LineItems)
                    {
                        var entry = entries.FirstOrDefault(x => x.Code == lineItem.Code);
                        if (entry == null)
                        {
                            //Entry was deleted, skip processing.
                            continue;
                        }

                        shipmentModel.CartItems.Add(CreateCartItemViewModel(cart, lineItem, entry));
                    }
                    model.Shipments.Add(shipmentModel);
                }    


            }

            return PartialView("CartSideBar", model);
        }

        [HttpPost]
        public async Task<ActionResult> AddToCart(string code)
        {
            ModelState.Clear();

            var contentLink = _referenceConverter.GetContentLink(code);
            var entryContent = _contentLoader.Get<EntryContentBase>(contentLink);

            var lineItem = Cart.GetAllLineItems().FirstOrDefault(x => x.Code == code && !x.IsGift);
            
            if (lineItem == null)
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

        public virtual CartItemViewModel CreateCartItemViewModel(ICart cart, ILineItem lineItem, EntryContentBase entry)
        {
            var viewModel = new CartItemViewModel
            {
                Code = lineItem.Code,
                DisplayName = entry.DisplayName ?? entry.Name,
                ImageUrl = entry.GetAssets<IContentImage>(_contentLoader, _urlResolver).FirstOrDefault() ?? "",
                //DiscountedPrice = GetDiscountedPrice(cart, lineItem),
                PlacedPrice = GetMoney(lineItem.PlacedPrice),
                Quantity = lineItem.Quantity,
                Url = entry.GetUrl(_relationRepository, _urlResolver),
                Entry = entry,
                IsAvailable = _priceService.GetPrices(
                                            _currentMarket.GetCurrentMarket().MarketId,
                                            DateTime.Now,
                                            new CatalogKey(entry.Code),
                                            new PriceFilter { Currencies = new[] { GetCurrentCurrency() } })
                                            .OrderBy(x => x.UnitPrice.Amount).FirstOrDefault() != null,
                //DiscountedUnitPrice = GetDiscountedUnitPrice(cart, lineItem),
                IsGift = lineItem.IsGift
            };

            var productLink = entry is VariationContent ?
                entry.GetParentProducts(_relationRepository).FirstOrDefault() :
                entry.ContentLink;

            //FashionProduct product;
            //if (_contentLoader.TryGet(productLink, out product))
            //{
            //    viewModel.Brand = GetBrand(product);
            //}

            //var variant = entry as FashionVariant;
            //if (variant != null)
            //{
            //    viewModel.AvailableSizes = GetAvailableSizes(product, variant);
            //}

            return viewModel;
        }

        public Money GetMoney(decimal amount)
        {
            return new Money(amount, GetCurrentCurrency());
        }

        private IEnumerable<ShippingMethodViewModel> CreateShippingMethodViewModels(MarketId marketId, Currency currency, IShipment shipment)
        {
            var market = _marketService.GetMarket(marketId);
            var shippingRates = GetShippingRates(market, currency, shipment);
            return shippingRates.Any()
                ? shippingRates.Select(r => new ShippingMethodViewModel { Id = r.Id, DisplayName = r.Name, Price = r.Money })
                : Enumerable.Empty<ShippingMethodViewModel>();
        }

        public AddressModel ConvertToModel(IOrderAddress orderAddress)
        {
            var address = new AddressModel();

            if (orderAddress != null)
            {
                MapToModel(orderAddress, address);
            }

            return address;
        }

        public void MapToModel(IOrderAddress orderAddress, AddressModel addressModel)
        {
            addressModel.AddressId = orderAddress.Id;
            addressModel.Name = orderAddress.Id;
            addressModel.Line1 = orderAddress.Line1;
            addressModel.Line2 = orderAddress.Line2;
            addressModel.City = orderAddress.City;
            addressModel.CountryName = orderAddress.CountryName;
            addressModel.CountryCode = orderAddress.CountryCode;
            addressModel.Email = orderAddress.Email;
            addressModel.FirstName = orderAddress.FirstName;
            addressModel.LastName = orderAddress.LastName;
            addressModel.PostalCode = orderAddress.PostalCode;
            addressModel.CountryRegion = new CountryRegionViewModel
            {
                Region = orderAddress.RegionName ?? orderAddress.RegionCode
            };
            addressModel.DaytimePhoneNumber = orderAddress.DaytimePhoneNumber;
        }

        private IEnumerable<ShippingRate> GetShippingRates(IMarket market, Currency currency, IShipment shipment)
        {
            var methods = GetShippingMethodsByMarket(market.MarketId.Value, false);
            var currentLanguage = GetCurrentLanguage().TwoLetterISOLanguageName;

            return methods.Where(shippingMethodRow => currentLanguage.Equals(shippingMethodRow.LanguageId, StringComparison.OrdinalIgnoreCase)
                && string.Equals(currency, shippingMethodRow.Currency, StringComparison.OrdinalIgnoreCase))
                .OrderBy(shippingMethodRow => shippingMethodRow.Ordering)
                .Select(shippingMethodRow => GetRate(shipment, shippingMethodRow, market))
                .Where(rate => rate != null);
        }

        public virtual ShippingRate GetRate(IShipment shipment, ShippingMethodInfoModel shippingMethodInfoModel, IMarket currentMarket)
        {
            var type = Type.GetType(shippingMethodInfoModel.ClassName);
            if (type == null)
            {
                throw new TypeInitializationException(shippingMethodInfoModel.ClassName, null);
            }
            string message = string.Empty;

            if(_shippingPlugins != null)
            {
                return _shippingPlugins.GetRate(currentMarket, shippingMethodInfoModel.MethodId, shipment, ref message);
            }
            
            if(_shippingGateways!=null)
            {
                return _shippingGateways.GetRate(currentMarket, shippingMethodInfoModel.MethodId, (Shipment)shipment, ref message);
            }
            
            throw new InvalidOperationException($"There is no registered {nameof(IShippingPlugin)} or {nameof(IShippingGateway)} instance.");
        }

        public virtual IList<ShippingMethodInfoModel> GetShippingMethodsByMarket(string marketid, bool returnInactive)
        {
            var methods = ShippingManager.GetShippingMethodsByMarket(marketid, returnInactive);
            return methods.ShippingMethod.Select(method => new ShippingMethodInfoModel
            {
                MethodId = method.ShippingMethodId,
                Currency = method.Currency,
                LanguageId = method.LanguageId,
                Ordering = method.Ordering,
                ClassName = methods.ShippingOption.FindByShippingOptionId(method.ShippingOptionId).ClassName
            }).ToList();
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

        public virtual CultureInfo GetCurrentLanguage()
        {
            CultureInfo cultureInfo;
            return TryGetLanguage(_cookieService.Get("Language"), out cultureInfo)
                ? cultureInfo
                : _currentMarket.GetCurrentMarket().DefaultLanguage;
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
        public virtual IEnumerable<CultureInfo> GetAvailableLanguages()
        {
            return _currentMarket.GetCurrentMarket().Languages;
        }

        private bool TryGetLanguage(string language, out CultureInfo cultureInfo)
        {
            cultureInfo = null;

            if (language == null)
            {
                return false;
            }

            try
            {
                var culture = CultureInfo.GetCultureInfo(language);
                cultureInfo = GetAvailableLanguages().FirstOrDefault(c => c.Name == culture.Name);
                return cultureInfo != null;
            }
            catch (CultureNotFoundException)
            {
                return false;
            }
        }
    }
}