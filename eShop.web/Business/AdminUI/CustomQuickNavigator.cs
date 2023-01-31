using EPiServer;
using EPiServer.Core;
using EPiServer.ServiceLocation;
using EPiServer.Web;
using EPiServer.Web.PageExtensions;
using eShop.web.Models.Pages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace eShop.web.Business.AdminUI
{
    [ServiceConfiguration(ServiceType = typeof(IQuickNavigatorItemProvider))]
    public class CustomQuickNavigator : IQuickNavigatorItemProvider
    {
        private int sortOrder = 50;

        public int SortOrder
        {
            get
            {
                return sortOrder;
            }
            set
            {
                sortOrder = value;
            }
        }

        public IDictionary<string, QuickNavigatorMenuItem> GetMenuItems(ContentReference currentContent)
        {
            var repository = ServiceLocator.Current.GetInstance<IContentRepository>();
            var startPage = repository.Get<StartPage>(PageReference.StartPage);

            var quickNavItem = new QuickNavigatorMenuItem("Homepage", startPage.LinkURL, null, null, null);

            var menuItems = new Dictionary<string, QuickNavigatorMenuItem>
                            {
                                {
                                    "qn-homepage-manager",
                                    quickNavItem
                                }
                            };

            return menuItems;
        }
    }
}