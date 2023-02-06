using EPiServer;
using EPiServer.Core;
using EPiServer.Framework.DataAnnotations;
using EPiServer.Web.Mvc;
using eShop.web.Models.Blocks;
using eShop.web.ViewModels;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace eShop.web.Controllers
{
    public class BigBannerLinkBlockController : BaseBlockController<BigBannerLinkBlock>
    {
        public override ActionResult Index(BigBannerLinkBlock currentBlock)
        {
            /* Implementation of action. You can create your own view model class that you pass to the view or
             * you can pass the page type for simpler templates */
            var viewmodel = BlockViewModel.Create(currentBlock, CurrentPage, CurrentPageLink);

            return PartialView(viewmodel);
        }
    }
}