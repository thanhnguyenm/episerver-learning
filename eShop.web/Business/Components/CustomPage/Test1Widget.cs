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
           WidgetType = "nitecoui/components/Test1Widget",
           Title = "Test1 Widget",
           SortOrder = 1200)]
    public class Test1Widget
    {
        public Test1Widget()
        {
            //var x = 5;
        }
    }
}