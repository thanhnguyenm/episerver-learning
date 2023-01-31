using EPiServer.Security;
using EPiServer.Shell.Navigation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace eShop.web.Business.AdminUI
{
    [MenuProvider]
    public class CustomCmsGlobalMenuProvider : IMenuProvider
    {
        IEnumerable<MenuItem> IMenuProvider.GetMenuItems()
        {
            return new List<MenuItem>
        {
                new UrlMenuItem("Hangfire", "/global/cms/Hangfire", "/CustomCmsGlobalHangfire")
                {
                    IsAvailable = request => PrincipalInfo.Current.RoleList.Any(x => x == "CutomRole") || PrincipalInfo.HasAdminAccess,
                    SortIndex = 100
                }
            };
        }
    }
}