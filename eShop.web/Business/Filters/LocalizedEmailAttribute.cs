using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace eShop.web.Business.Filters
{
    public class LocalizedEmailAttribute : LocalizedRegularExpressionAttribute
    {
        public LocalizedEmailAttribute(string name)
            : base(@"^([0-9a-zA-Z]([-.\w]*[0-9a-zA-Z])*@([0-9a-zA-Z][-\w]*[0-9a-zA-Z]\.)+[a-zA-Z]{2,9})$", name)
        {
        }
    }
}