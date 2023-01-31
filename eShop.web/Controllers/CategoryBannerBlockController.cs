using eShop.web.Models.Blocks;
using eShop.web.ViewModels;
using System.Web.Mvc;

namespace eShop.web.Controllers
{
    public class CategoryBannerBlockController : BaseBlockController<CategoryBannerBlock>
    {
        public override ActionResult Index(CategoryBannerBlock currentBlock)
        {
            var viewmodel = BlockViewModel.Create(currentBlock, CurrentPage, CurrentPageLink);

            // var myProperty = ControllerContext.ParentActionViewContext.ViewData\["CustomParamter"\]?.ToString() ?? string.Empty; =>access pametters are passed from PropertyFor of ContentArea


            return PartialView(viewmodel);
        }
    }
}
