using EPiServer.Shell;
using EPiServer.Shell.ViewComposition;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace eShop.web.Business.Components.CustomPage
{
    [Component(PlugInAreas = PlugInArea.Assets,
           Categories = "cms",
           WidgetType = "nitecoui/components/CustomPanelWidget",
           Title = "Custom Page Widget",
           SortOrder = 1100)]
    public class CustomPanelWidget
    {
        public CustomPanelWidget()
        {
            var x = 5;
        }
    }
}