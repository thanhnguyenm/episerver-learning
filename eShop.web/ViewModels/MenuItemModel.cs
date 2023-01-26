using EPiServer.Core;
using System;

namespace eShop.web.ViewModels
{
    public class MenuItemModel
    {
        public MenuItemModel(PageData page)
        {
            Page = page;
        }
        public PageData Page { get; set; }
        public bool Selected { get; set; }
        public Lazy<bool> HasChildren { get; set; }
    }
}