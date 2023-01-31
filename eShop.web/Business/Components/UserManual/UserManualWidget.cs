using EPiServer.Shell;
using EPiServer.Shell.ViewComposition;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace eShop.web.Business.Components.UserManual
{
    [Component(PlugInAreas = PlugInArea.Assets,
           Categories = "cms",
           WidgetType = "nitecoui/components/UserManualWidget",
           Title = "User Manual",
           SortOrder = 1200)]
    public class UserManualWidget
    {
        public UserManualWidget()
        {
            var y = 4;
        }
    }
}