using EPiServer.Web.Mvc;
using eShop.web.Models.Blocks;
using System.Web.Mvc;

namespace eShop.web.Controllers
{
    public class ShippingAddressBlockController : BlockController<ShippingAddressBlock>
    {
        public override ActionResult Index(ShippingAddressBlock currentBlock)
        {
            return PartialView(currentBlock);
        }
    }
}
