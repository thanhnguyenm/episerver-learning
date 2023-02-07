using EPiServer;
using EPiServer.Core;
using EPiServer.Framework.DataAnnotations;
using EPiServer.Web;
using EPiServer.Web.Mvc;
using EPiServer.Web.Routing;
using eShop.web.Business.Filters;
using eShop.web.Models.Pages;
using eShop.web.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace eShop.web.Controllers
{
    public class LoginPageController : BasePageController<LoginPage>
    {
        [ImportModelStateFromTempDataFilter]
        public ActionResult Index(LoginPage currentPage)
        {
            /* Implementation of action. You can create your own view model class that you pass to the view or
             * you can pass the page type for simpler templates */

            var model = PageViewModel.Create(currentPage);

            return View(model);
        }

        [ExportModelStateToTempDataFilter]
        [HttpPost]
        public ActionResult Login(LoginModel model, PageReference currentPageLink)
        {
            var returnUrl = UrlResolver.Current.GetUrl(currentPageLink);

            if (ModelState.IsValid && Membership.ValidateUser(model.Username, model.Password))
            {
                string userData = "ApplicationSpecific data for this user.";
                FormsAuthenticationTicket ticket = new FormsAuthenticationTicket(1,
                    model.Username,
                    DateTime.Now,
                    DateTime.Now.AddMinutes(30),
                    true,
                    userData,
                    FormsAuthentication.FormsCookiePath);

                //FormsAuthentication.SetAuthCookie(model.Username, true);
                // Encrypt the ticket.
                string encTicket = FormsAuthentication.Encrypt(ticket);

                // Create the cookie.
                Response.Cookies.Add(new HttpCookie(FormsAuthentication.FormsCookieName, encTicket));

                return Redirect("/");
            }
            else
            {
                ModelState.AddModelError(string.Empty, "Log-in Failed");
                SaveModelState(currentPageLink, model);
            }


            return Redirect(returnUrl);
        }

        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();
            var returnUrl = UrlResolver.Current.GetUrl(SiteDefinition.Current.StartPage);
            return Redirect(returnUrl);
        }

        protected virtual void SaveModelState(ContentReference page, object formModel)
        {
            if (ViewData.ModelState != null && !ViewData.ModelState.IsValid)
            {
                TempData[StateKey(page)] = ViewData.ModelState;
                TempData[StateKey(page) + "_Model"] = formModel;
            }
        }

        protected virtual void LoadModelState(ContentReference page, out object formModel)
        {
            var key = StateKey(page);
            var keymodel = key + "_Model";
            var modelState = TempData[key] as ModelStateDictionary;
            formModel = null;
            if (modelState != null)
            {
                ViewData.ModelState.Merge(modelState);
                TempData.Remove(key);

                formModel = TempData[keymodel];
                TempData.Remove(keymodel);
            }
        }

        private static string StateKey(ContentReference page)
        {
            return typeof(LoginPageController).FullName + $"_{page.ID}";
        }
    }
}