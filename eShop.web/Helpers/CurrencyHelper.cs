using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace eShop.web.Helpers
{
    public static class CurrencyHelper
    {
        public static string CurrenyFormat(this decimal value)
        {
            return $"$ {value:#.00}";
        }
    }
}