using EPiServer.Web.Mvc;
using eShop.web.Models.Blocks;
using System.Web.Mvc;

namespace eShop.web.Controllers
{
    public class ColourBlockController : BlockController<ColourBlock>
    {
        public override ActionResult Index(ColourBlock currentBlock)
        {

            return PartialView(currentBlock);
        }
    }
}
