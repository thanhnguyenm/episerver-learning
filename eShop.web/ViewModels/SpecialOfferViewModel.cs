using eShop.web.Models.Blocks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace eShop.web.ViewModels
{
    public class SpecialOfferViewModel : BlockViewModel<SpecialOfferBlock>
    {
        public SpecialOfferViewModel(SpecialOfferBlock currentBlock) :  base(currentBlock)
        {

        }

        public List<ProductContentViewModel> ProductContentViewModels { get; set; }
    }
}