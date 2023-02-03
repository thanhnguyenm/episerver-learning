using Hangfire.Annotations;
using Hangfire.Dashboard;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace eShop.web.Business.Filters
{
    public class HangFireDashboardAttribute : IDashboardAuthorizationFilter
    {
        public bool Authorize([NotNull] DashboardContext context)
        {
            return EPiServer.Security.PrincipalInfo.HasAdminAccess;
        }
    }
}