using Mediachase.Commerce;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eShop.web.Models.Commerce
{
    interface IProductModel
    {
        string Brand { get; set; }
        string Code { get; set; }
        string DisplayName { get; set; }
        Money? DiscountedPrice { get; set; }
        string ImageUrl { get; set; }
        Money PlacedPrice { get; set; }
        string Url { get; set; }
        bool IsAvailable { get; set; }
    }
}
