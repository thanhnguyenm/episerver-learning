using EPiServer.Framework.Localization;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace eShop.web.Business.Filters
{
    public class LocalizedRequiredAttribute : RequiredAttribute
    {
        private readonly string _translationPath;

        public LocalizedRequiredAttribute(string translationPath)
        {
            _translationPath = translationPath;
        }

        public override string FormatErrorMessage(string name)
        {
            ErrorMessage = LocalizationService.Current.GetString(_translationPath);
            return base.FormatErrorMessage(name);
        }
    }
}