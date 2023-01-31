using EPiServer;
using EPiServer.Core;
using EPiServer.Framework;
using EPiServer.Framework.Initialization;
using EPiServer.ServiceLocation;
using eShop.web.Helpers;
using eShop.web.Models.Pages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace eShop.web.Business.Initialization
{
    [InitializableModule]
    [ModuleDependency(typeof(EPiServer.Web.InitializationModule))]
    //[ModuleDependency(typeof(EPiServer.Web.InitializationModule), typeof(EPiServer.Commerce.Initialization.InitializationModule))]
    public class EventsInitialization : IInitializableModule
    {
        public void Initialize(InitializationEngine context)
        {
            //var events = context.Locate.ContentEvents(); => or
            var events = ServiceLocator.Current.GetInstance<IContentEvents>();
            events.PublishedContent += EventsPublishedContent; //hook into PublishedContent event
            events.DeletingContent += DeletingContent;
            events.MovingContent += MovingContent;
        }

        public void Preload(string[] parameters)
        {
        }

        public void Uninitialize(InitializationEngine context)
        {
        }

        void EventsPublishedContent(object sender, ContentEventArgs e)
        {
            var repo = ServiceLocator.Current.GetInstance<IContentRepository>(); 
            var content = repo.Get<IContent>(e.ContentLink);
            NotificationCentre.Instance.AddNewNotification(new ViewModels.Notification { 
                Message = $"content {content.Name} is just pblished",
                Date = DateTime.Now,
                IsRead = false
            });

            if (content.GetType() == typeof(StartPage))
            {
                string title = (content as StartPage).MetaTitle;
            }
        }

        private void DeletingContent(object sender, ContentEventArgs e)
        {
            if (e.Content is StartPage)
            {
                e.CancelAction = true; // don't allow delete
                e.CancelReason = "Not Allowed To Delete PageType";
            }
        }

        private void MovingContent(object sender, ContentEventArgs e)
        {
            if (e.Content is StartPage && e.TargetLink.ID == 2) // move to Trash
            {
                e.CancelAction = true;
                e.CancelReason = "Not Allowed To Delete PageType";
            }
        }
    }
}