using System.Web.Mvc;

namespace eShop.web.Business.Filters
{
    public abstract class ModelStateTempDataTransferFilter : ActionFilterAttribute
    {
        protected static readonly string Key = typeof(ModelStateTempDataTransferFilter).FullName;
    }
}