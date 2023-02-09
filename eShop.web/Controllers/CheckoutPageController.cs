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
using Mediachase.Commerce.Pricing;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web.Mvc;

namespace eShop.web.Controllers
{
    public class CheckoutPageController : BasePageController<CheckoutPage>
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

        public CheckoutPageController()
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

        public ActionResult Index(CheckoutPage currentPage)
        {
            /* Implementation of action. You can create your own view model class that you pass to the view or
             * you can pass the page type for simpler templates */
            
            var model = new CheckoutViewModel
            {
                //CurrentPage = currentPage,
                Shipments = new List<ShipmentViewModel>(),
                //AppliedCouponCodes = new List<string>(),
                CurrentPage = currentPage,
                StartPage = _contentLoader.Get<StartPage>(ContentReference.StartPage),
                AvailableAddresses = new List<AddressModel>(),
                UseBillingAddressForShipment = true
            };

            //if (Cart != null && Cart.GetAllLineItems().Any())
            {
                var currentShippingAddressId = Cart.GetFirstShipment()?.ShippingAddress?.Id;
                var currentBillingAdressId = Cart.GetFirstForm().Payments.FirstOrDefault()?.BillingAddress?.Id;

                foreach (var shipment in Cart.GetFirstForm().Shipments)
                {
                    var shipmentModel = new ShipmentViewModel
                    {
                        ShipmentId = shipment.ShipmentId,
                        CartItems = new List<CartItemViewModel>(),
                        Address = ConvertToModel(shipment.ShippingAddress),
                        ShippingMethods = CreateShippingMethodViewModels(Cart.MarketId, Cart.Currency, shipment)
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

                        shipmentModel.CartItems.Add(CreateCartItemViewModel(Cart, lineItem, entry));
                    }
                    model.Shipments.Add(shipmentModel);
                }

                var customer = CustomerContext.Current.CurrentContact;
                var useBillingAddressForShipment =
                       model.Shipments.Count == 1 && currentBillingAdressId == currentShippingAddressId &&
                       (customer == null ||
                       customer.PreferredShippingAddressId.HasValue &&
                       customer.PreferredShippingAddressId == customer.PreferredBillingAddressId);

                model.BillingAddress = CreateBillingAddressModel();
                model.BillingAddress.CountryOptions = CountryManager.GetCountries().Country.Select(x => new CountryViewModel { Code = x.Code, Name = x.Name });

                model.UseBillingAddressForShipment = useBillingAddressForShipment;
                model.AvailableAddresses = AddressList();

                UpdateShippingAddresses(Cart, model);
                UpdateShippingMethods(Cart, model.Shipments);

                //_cartService.ApplyDiscounts(Cart);
                _orderRepository.Save(Cart);
            }

            return View(model);
        }

        [HttpPost]
        public ActionResult Purchase(CheckoutViewModel viewModel)
        {
            if (Cart == null || !Cart.GetAllLineItems().Any())
            {
                return Redirect(Url.ContentUrl(ContentReference.StartPage));
            }    

            //address
            viewModel.BillingAddress.CountryOptions = CountryManager.GetCountries().Country.Select(x => new CountryViewModel { Code = x.Code, Name = x.Name });
            viewModel.BillingAddress.CountryRegion.RegionOptions = Enumerable.Empty<string>();
            var issues = _orderValidationService.ValidateOrder(Cart);

            // validate email in billding address
            //..

            // use shipping same billing address
            Cart.GetFirstShipment().ShippingAddress = ConvertToAddress(viewModel.BillingAddress, Cart);

            //create payment
            foreach (var form in Cart.Forms)
            {
                form.Payments.Clear();
            }

            var method = GetPaymentMethod().FirstOrDefault(x => x.SystemKeyword == viewModel.SystemKeyword);

            var total = Cart.GetTotal(_orderGroupCalculator);
            var payment = CreatePayment(total.Amount, Cart, method.PaymentMethodId, viewModel.SystemKeyword);
            Cart.AddPayment(payment, _orderGroupFactory);
            payment.BillingAddress = ConvertToAddress(viewModel.BillingAddress, Cart);

            //process
            var paymentProcessingResults = Cart.ProcessPayments(_paymentProcessor, _orderGroupCalculator).ToList();
            if (paymentProcessingResults.Any(r => !r.IsSuccessful))
            {
                ModelState.AddModelError("", _localizationService.GetString("/Checkout/Payment/Errors/ProcessingPaymentFailure") + string.Join(", ", paymentProcessingResults.Select(p => p.Message)));
                return null;
            }

            var redirectPayment = paymentProcessingResults.FirstOrDefault(r => !string.IsNullOrEmpty(r.RedirectUrl));
            if (redirectPayment != null)
            {
                return null;
            }

            var processedPayments = Cart.GetFirstForm().Payments.Where(x => x.Status.Equals(PaymentStatus.Processed.ToString())).ToList();
            if (!processedPayments.Any())
            {
                // Return null in case there is no payment was processed.
                return null;
            }

            var totalProcessedAmount = processedPayments.Sum(x => x.Amount);
            if (totalProcessedAmount != Cart.GetTotal(_orderGroupCalculator).Amount)
            {
                throw new InvalidOperationException("Wrong amount");
            }

            var orderReference = _orderRepository.SaveAsPurchaseOrder(Cart);
            var purchaseOrder = _orderRepository.Load<IPurchaseOrder>(orderReference.OrderGroupId);
            _orderRepository.Delete(Cart.OrderLink);

            if(purchaseOrder == null)
            {
                return null;
            }

            return View("~/Views/CheckoutPage/Purchase.cshtml", purchaseOrder);
        }

        //get payments
        public IEnumerable<PaymentMethodViewModel> GetPaymentMethod()
        {
            var currentMarket = _currentMarket.GetCurrentMarket().MarketId;
            var currentLanguage = GetCurrentLanguage().TwoLetterISOLanguageName;
            //PaymentManager.GetPaymentMethodBySystemName
            var methods = PaymentManager.GetPaymentMethodsByMarket(currentMarket.Value)
                .PaymentMethod
                .Where(x => x.IsActive && currentLanguage.Equals(x.LanguageId, StringComparison.OrdinalIgnoreCase))
                .OrderBy(x => x.Ordering)
                .Select(x => new PaymentMethodViewModel
                {
                    PaymentMethodId = x.PaymentMethodId,
                    SystemKeyword = x.SystemKeyword,
                    PaymentMethodName = x.Name,
                    IsDefault = x.IsDefault
                });

            return methods;
        }

        public IPayment CreatePayment(decimal amount, IOrderGroup orderGroup, Guid PaymentMethodId, string SystemKeyword)
        {
            var payment = orderGroup.CreatePayment(_orderGroupFactory);
            payment.PaymentType = PaymentType.Other;
            payment.PaymentMethodId = PaymentMethodId;
            payment.PaymentMethodName = SystemKeyword;
            payment.Amount = amount;
            payment.Status = PaymentStatus.Pending.ToString();
            payment.TransactionType = TransactionType.Sale.ToString();
            return payment;
        }

        public virtual void UpdateShippingAddresses(ICart cart, CheckoutViewModel viewModel)
        {
            if (viewModel.UseBillingAddressForShipment)
            {
                cart.GetFirstShipment().ShippingAddress = ConvertToAddress(viewModel.BillingAddress, cart);
            }
            else
            {
                var shipments = cart.GetFirstForm().Shipments;
                for (var index = 0; index < shipments.Count; index++)
                {
                    shipments.ElementAt(index).ShippingAddress = ConvertToAddress(viewModel.Shipments[index].Address, cart);
                }
            }
        }

        public virtual void UpdateShippingMethods(ICart cart, IList<ShipmentViewModel> shipmentViewModels)
        {
            var index = 0;
            foreach (var shipment in cart.GetFirstForm().Shipments)
            {
                shipment.ShippingMethodId = shipmentViewModels[index++].ShippingMethodId;
            }
        }

        public IOrderAddress ConvertToAddress(AddressModel model, IOrderGroup orderGroup)
        {
            var address = orderGroup.CreateOrderAddress(_orderGroupFactory, model.Name);
            MapToAddress(model, address);

            return address;
        }

        public void MapToAddress(AddressModel addressModel, IOrderAddress orderAddress)
        {
            orderAddress.Id = addressModel.Name;
            orderAddress.City = addressModel.City;
            orderAddress.CountryCode = addressModel.CountryCode;
            orderAddress.CountryName = CountryManager.GetCountries().Country.Where(x => x.Code == addressModel.CountryCode).Select(x => x.Name).FirstOrDefault();
            orderAddress.FirstName = addressModel.FirstName;
            orderAddress.LastName = addressModel.LastName;
            orderAddress.Line1 = addressModel.Line1;
            orderAddress.Line2 = addressModel.Line2;
            orderAddress.DaytimePhoneNumber = addressModel.DaytimePhoneNumber;
            orderAddress.PostalCode = addressModel.PostalCode;
            orderAddress.RegionName = addressModel.CountryRegion.Region;
            orderAddress.RegionCode = addressModel.CountryRegion.Region;
            orderAddress.Email = addressModel.Email;
            orderAddress.Organization = addressModel.Organization;
        }

        public IList<AddressModel> AddressList()
        {
            var currentContact = CustomerContext.Current.CurrentContact;
            var addresses = new List<AddressModel>();

            if (currentContact != null)
            {
                addresses.AddRange(currentContact.ContactAddresses.Select(customerAddress => new AddressModel
                {
                    AddressId = customerAddress.Name,
                    Name = customerAddress.Name,
                    FirstName = customerAddress.FirstName,
                    LastName = customerAddress.LastName,
                    Line1 = customerAddress.Line1,
                    Line2 = customerAddress.Line2,
                    PostalCode = customerAddress.PostalCode,
                    City = customerAddress.City,
                    CountryCode = customerAddress.CountryCode,
                    CountryName = customerAddress.CountryName,
                    CountryRegion = new CountryRegionViewModel()
                    {
                        Region = customerAddress.RegionName ?? customerAddress.RegionCode ?? customerAddress.State
                    },
                    Email = customerAddress.Email,
                    ShippingDefault = currentContact.PreferredShippingAddress != null
                                            && customerAddress.AddressId == currentContact.PreferredShippingAddressId,
                    BillingDefault = currentContact.PreferredBillingAddress != null
                                            && customerAddress.AddressId == currentContact.PreferredBillingAddressId
                }));
            }

            return addresses;
        }

        private AddressModel CreateBillingAddressModel()
        {
            var preferredBillingAddress = CustomerContext.Current.CurrentContact?.PreferredBillingAddress;
            var addressId = preferredBillingAddress?.Name ?? Guid.NewGuid().ToString();

            return new AddressModel
            {
                AddressId = addressId,
                Name = addressId
            };
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

        //public virtual void UpdateShippingAddresses(ICart cart, CheckoutViewModel viewModel)
        //{
        //    if (viewModel.UseBillingAddressForShipment)
        //    {
        //        cart.GetFirstShipment().ShippingAddress = _addressBookService.ConvertToAddress(viewModel.BillingAddress, cart);
        //    }
        //    else
        //    {
        //        var shipments = cart.GetFirstForm().Shipments;
        //        for (var index = 0; index < shipments.Count; index++)
        //        {
        //            shipments.ElementAt(index).ShippingAddress = _addressBookService.ConvertToAddress(viewModel.Shipments[index].Address, cart);
        //        }
        //    }
        //}

        private IEnumerable<ShippingMethodViewModel> CreateShippingMethodViewModels(MarketId marketId, Currency currency, IShipment shipment)
        {
            var market = _marketService.GetMarket(marketId);
            var shippingRates = GetShippingRates(market, currency, shipment);
            return shippingRates.Any()
                ? shippingRates.Select(r => new ShippingMethodViewModel { Id = r.Id, DisplayName = r.Name, Price = r.Money })
                : Enumerable.Empty<ShippingMethodViewModel>();
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

            if (_shippingPlugins != null)
            {
                return _shippingPlugins.GetRate(currentMarket, shippingMethodInfoModel.MethodId, shipment, ref message);
            }

            if (_shippingGateways != null)
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

        public virtual IEnumerable<CultureInfo> GetAvailableLanguages()
        {
            return _currentMarket.GetCurrentMarket().Languages;
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

        private ICart _cart;
        private ICart Cart
        {
            get { return _cart ?? (_cart = _orderRepository.LoadOrCreateCart<ICart>(CustomerContext.Current.CurrentContactId, "CartDefault")); }
        }
    }
}