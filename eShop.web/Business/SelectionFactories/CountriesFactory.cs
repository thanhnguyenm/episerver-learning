using EPiServer.Shell.ObjectEditing;
using Mediachase.Commerce.Orders.Managers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace eShop.web.Business.SelectionFactories
{
    public class CountriesFactory : ISelectionFactory
    {
        public virtual IEnumerable<ISelectItem> GetSelections(ExtendedMetadata metadata)
        {
            var countries = CountryManager.GetCountries().Country.Select(x => new SelectItem { Value = x.Code, Text = x.Name });
            return countries;
        }

    }
}