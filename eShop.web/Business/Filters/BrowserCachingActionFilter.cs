using System;
using System.Web;
using System.Web.Mvc;

namespace eShop.web.Business.Filters
{
    public class BrowserCachingActionFilter : IResultFilter
    {
        public void OnResultExecuted(ResultExecutedContext filterContext)
        {
            
        }

        public void OnResultExecuting(ResultExecutingContext filterContext)
        {
            filterContext.HttpContext.Response.Cache.SetExpires(DateTime.Now.AddMinutes(2.0));
            filterContext.HttpContext.Response.Cache.SetCacheability(HttpCacheability.Public);
            filterContext.HttpContext.Response.Cache.SetValidUntilExpires(true);
        }
    }
}