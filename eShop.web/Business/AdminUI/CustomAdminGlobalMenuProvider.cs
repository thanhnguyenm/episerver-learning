using EPiServer;
using EPiServer.Security;
using EPiServer.Shell.Navigation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Routing;

namespace eShop.web.Business.AdminUI
{
    [MenuProvider]
    public class CustomAdminGlobalMenuProvider : IMenuProvider
    {
        public IEnumerable<MenuItem> GetMenuItems()
        {
            var mainAdminMenu = new SectionMenuItem("Admin", "/global/admin");
            mainAdminMenu.IsAvailable = ((RequestContext request) => PrincipalInfo.Current.HasPathAccess(EPiServer.Web.UriUtil.Combine("/CustomAdminGlobalMainPage", "")));

            var firstMenuItem = new UrlMenuItem("Main", "/global/admin/main", "/CustomAdminGlobalMainPage/");
            firstMenuItem.IsAvailable = ((RequestContext request) => true);
            firstMenuItem.SortIndex = 100;

            var secondMenuItem = new UrlMenuItem("Second", "/global/admin/main", "/CustomAdminGlobalMainPage/");
            secondMenuItem.IsAvailable = ((RequestContext request) => true);
            secondMenuItem.SortIndex = 101;

            return new MenuItem[]
            {
                mainAdminMenu,
                firstMenuItem,
                secondMenuItem
            };
        }
    }
}