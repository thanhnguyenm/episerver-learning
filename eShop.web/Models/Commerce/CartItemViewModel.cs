using EPiServer.Commerce.Catalog.ContentTypes;
using Mediachase.Commerce;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace eShop.web.Models.Commerce
{
    public class CartItemViewModel : IProductModel
    {
        public string DisplayName { get; set; }

        public string ImageUrl { get; set; }

        public string Url { get; set; }

        public string Brand { get; set; }

        public Money? DiscountedPrice { get; set; }

        public Money PlacedPrice { get; set; }

        public string Code { get; set; }

        public EntryContentBase Entry { get; set; }

        public decimal Quantity { get; set; }

        public Money? DiscountedUnitPrice { get; set; }

        public IEnumerable<string> AvailableSizes { get; set; }

        public bool IsAvailable { get; set; }

        public string AddressId { get; set; }

        public bool IsGift { get; set; }
    }
}