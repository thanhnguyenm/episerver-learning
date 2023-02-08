using EPiServer.Framework.Localization;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;

namespace eShop.web.Business.Filters
{
    public class LocalizedDisplayAttribute : DisplayNameAttribute
    {
        public LocalizedDisplayAttribute(string displayNameKey)
            : base(displayNameKey)
        {
        }

        public override string DisplayName
        {
            get
            {
                var s = LocalizationService.Current.GetString(base.DisplayName);
                return string.IsNullOrWhiteSpace(s) ? base.DisplayName : s;
            }
        }
    }
}