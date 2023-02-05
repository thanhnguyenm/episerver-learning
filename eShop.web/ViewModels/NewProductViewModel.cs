using eShop.web.Models.Blocks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace eShop.web.ViewModels
{
    public class NewProductViewModel : BlockViewModel<NewProductBlock>
    {
        public NewProductViewModel(NewProductBlock currentBlock) : base(currentBlock, string.Empty)
        {
        }

        public NewProductViewModel(NewProductBlock currentBlock, string displayOptionTag) : base(currentBlock, displayOptionTag)
        {   
        }

        public List<CatalogContentViewModel> Catalogs { get; set; }
    }
}