using eShop.web.Business.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace eShop.web.Models.Commerce
{
    public class CountryRegionViewModel
    {
        public IEnumerable<string> RegionOptions { get; set; }

        [LocalizedDisplay("/Shared/Address/Form/Label/CountryRegion")]
        public string Region { get; set; }
    }
}