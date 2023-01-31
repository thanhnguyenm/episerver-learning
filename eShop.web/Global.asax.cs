using System;
using System.Globalization;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace eShop.web
{
    public class EPiServerApplication : EPiServer.Global
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();

            //Tip: Want to call the EPiServer API on startup? Add an initialization module instead (Add -> New Item.. -> EPiServer -> Initialization Module)
        }

        protected override void RegisterRoutes(RouteCollection routes)
        {
            base.RegisterRoutes(routes);

            RouteTable.Routes.MapRoute("DebuggingInformation", "DebuggingInformation", new { controller = "DebuggingInformation", action = "Index" });
            RouteTable.Routes.MapRoute("CustomAdminGlobalMainPage", "CustomAdminGlobalMainPage", new { controller = "CustomAdminGlobalMainPage", action = "Index" });
            RouteTable.Routes.MapRoute("CustomCmsGlobalHangfire", "CustomCmsGlobalHangfire", new { controller = "CustomCmsGlobalHangfire", action = "Index" });
            RouteTable.Routes.MapRoute("defaultRoute", "{controller}/{action}");
        }


        /// <summary>
        /// When URLs donot contain language code, we can cache content by language in request (from browser)
        /// follow Episerver config
        /// </summary>
        /// <param name="context"></param>
        /// <param name="custom"></param>
        /// <returns></returns>
        //public override string GetVaryByCustomString(HttpContext context, string custom)
        //{
        //    try
        //    {
        //        if ("CountryCodeHash".Equals(custom))
        //        {
        //            var languages = HttpContext.Current.Request.UserLanguages;
        //            if (languages == null || languages.Length == 0)
        //                return null;

        //            var language = languages[0].ToLowerInvariant().Trim();
        //            return CultureInfo.CreateSpecificCulture(language).LCID.ToString();
        //        }
        //    }
        //    catch
        //    {
        //    }

        //    return base.GetVaryByCustomString(context, custom);
        //}
    }
}