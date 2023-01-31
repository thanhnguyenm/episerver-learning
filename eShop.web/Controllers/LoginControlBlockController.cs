using EPiServer.Core;
using EPiServer.Web;
using EPiServer.Web.Mvc;
using EPiServer.Web.Routing;
using eShop.web.Business.Filters;
using eShop.web.Models.Blocks;
using eShop.web.Models.Pages;
using eShop.web.ViewModels;
using System.Web.Mvc;
using System.Web.Security;

namespace eShop.web.Controllers
{
    public class LoginControlBlockController : BaseFormBlockController<LoginControlBlock>
    {
        //[ImportModelStateFromTempDataFilter]
        public override ActionResult Index(LoginControlBlock currentBlock)
        {
            var viewModel = new LoginControlBlockViewModel(currentBlock)
            {
                CurrentPage = CurrentPage,
                CurrentPageLink = CurrentPageLink
            };

            LoadModelState(viewModel.CurrentBlockLink, out var oldModel);
            viewModel.CurrentBlock.LoginModel = (LoginModel)oldModel;

            return PartialView(viewModel);
        }

        //[ExportModelStateToTempDataFilter]
        [HttpPost]
        public ActionResult Login(LoginModel model, PageReference currentPageLink, ContentReference currentBlockLink)
        {
            var returnUrl = UrlResolver.Current.GetUrl(currentPageLink);

            if (ModelState.IsValid && Membership.ValidateUser(model.Username, model.Password))
            {
                FormsAuthentication.SetAuthCookie(model.Username, model.RememberMe);
            }
            else
            {
                ModelState.AddModelError(string.Empty, "Log-in Failed");
                SaveModelState(currentBlockLink, model);
            }


            return Redirect(returnUrl);
        }

        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();
            var returnUrl = UrlResolver.Current.GetUrl(SiteDefinition.Current.StartPage);
            return Redirect(returnUrl);
        }
    }
}
