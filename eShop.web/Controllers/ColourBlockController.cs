using EPiServer.Web.Mvc;
using eShop.web.Models.Blocks;
using eShop.web.ViewModels;
using System.Web.Mvc;

namespace eShop.web.Controllers
{
    public class ColourBlockController : BaseBlockController<ColourBlock>
    {
        public override ActionResult Index(ColourBlock currentBlock)
        {
            var viewmodel = BlockViewModel.Create(currentBlock, CurrentPage, CurrentPageLink);

            return PartialView(viewmodel);
        }
    }
}
