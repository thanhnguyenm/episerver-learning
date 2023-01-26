using eShop.web.Models.Pages;

namespace eShop.web.ViewModels
{
    public interface IPageViewModel<out T> where T : SitePageData
    {
        T CurrentPage { get; }
        LayoutViewModel Layout { get; set; }
    }
}
