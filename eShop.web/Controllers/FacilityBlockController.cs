using EPiServer;
using EPiServer.Core;
using EPiServer.Web;
using EPiServer.Web.Mvc;
using eShop.web.Models.Blocks;
using eShop.web.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace eShop.web.Controllers
{
    public class FacilityBlockController : BaseBlockController<ServiceBlock>
    {
        public override ActionResult Index(ServiceBlock currentBlock)
        {
            var viewmodel = BlockViewModel.Create(currentBlock, CurrentPage, CurrentPageLink);

            return PartialView(viewmodel);
        }
    }
}
