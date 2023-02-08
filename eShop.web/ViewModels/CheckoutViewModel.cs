using EPiServer.Commerce.Order;
using eShop.web.Models.Commerce;
using eShop.web.Models.Pages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace eShop.web.ViewModels
{
    public class CheckoutViewModel
    {
        public StartPage StartPage { get; set; }


        /// <summary>
        /// Gets or sets all existing shipments related to the current order.
        /// </summary>
        public IList<ShipmentViewModel> Shipments { get; set; }

        /// <summary>
        /// Gets or sets the billing address.
        /// </summary>
        public AddressModel BillingAddress { get; set; }

        /// <summary>
        /// Gets or sets the payment method associated to the current purchase.
        /// </summary>
        public IPaymentMethod Payment { get; set; }

        /// <summary>
        /// Gets or sets whether the shipping address should be the same as the billing address.
        /// </summary>
        public bool UseBillingAddressForShipment { get; set; }

        /// <summary>
        /// Gets or sets the view message.
        /// </summary>
        public string Message { get; set; }

        /// <summary>
        /// Gets or sets whether the user is anthenticated or anonymous.
        /// </summary>
        public bool IsAuthenticated { get; set; }

        /// <summary>
        /// Gets or sets a list of all existing addresses for the current customer and that can be used for billing and shipment.
        /// </summary>
        public IList<AddressModel> AvailableAddresses { get; set; }
    }
}