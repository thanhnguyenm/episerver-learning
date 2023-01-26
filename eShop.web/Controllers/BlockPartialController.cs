using EPiServer.Core;
using EPiServer.Framework.DataAnnotations;
using EPiServer.Framework.Web;
using EPiServer.Web.Mvc;
using eShop.web.Models.Blocks;
using System.Web.Mvc;

namespace eShop.web.Controllers
{
    /// <summary>
    /// This example is to use one controller handle all blocks without their own controllers.
    /// Or we can use TemplateCoordinator  : IViewTemplateModelRegistrator like Alloy template
    /// We comment out because we don't want it impact other. We can use PartialContentController for a page rendered as a partial 
    /// </summary>
    /// <typeparam name="T"></typeparam>

    [TemplateDescriptor(TemplateTypeCategory = TemplateTypeCategories.MvcPartialController, Inherited = true, AvailableWithoutTag = false)]
    public class BasePartialController<T> : PartialContentController<T> where T : IContentData
    {
        public BasePartialController()
        {
        }
    }

    //public class BlockPartialController : BasePartialController<SiteBlockData>
    //{

    //    private const string PARTIAL_VIEW_DIRECTORY = "~/Views/Shared/Blocks/";

    //    public override ActionResult Index(BlockData blockData)
    //    {
    //        var viewName = string.Empty;

    //        if (blockData == typeof(PromoBlock))
    //        {
    //            viewName = "promo";
    //        }

    //        return PartialView(string.Format("{0}{1}.cshtml", PARTIAL_VIEW_DIRECTORY, viewName));
    //    }
    //}
}
