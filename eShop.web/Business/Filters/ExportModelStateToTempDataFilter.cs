using System.Web.Mvc;

namespace eShop.web.Business.Filters
{
    public class ExportModelStateToTempDataFilter : ModelStateTempDataTransferFilter
    {
        public override void OnActionExecuted(ActionExecutedContext filterContext)
        {
            if (!filterContext.Controller.ViewData.ModelState.IsValid)
            {
                if ((filterContext.Result is RedirectResult) || (filterContext.Result is RedirectToRouteResult))
                    filterContext.Controller.TempData[Key] = filterContext.Controller.ViewData.ModelState;
            }

            base.OnActionExecuted(filterContext);
        }
    }
}