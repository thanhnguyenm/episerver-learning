using eShop.web.Helpers;
using eShop.web.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace eShop.web.Business.Hubs
{
    public class NotificationHub : Microsoft.AspNet.SignalR.Hub
    {
        private readonly NotificationCentre notificationCentre;

        public NotificationHub() : this(NotificationCentre.Instance)
        {

        }

        public NotificationHub(NotificationCentre notificationCentre)
        {
            this.notificationCentre = notificationCentre;
        }

        public void AddNewNotification(string message)
        {
            var notification = new Notification
            {
                Message = message,
                Date = DateTime.Now,
                IsRead = false
            };

            notificationCentre.AddNewNotification(notification);
        }

        public IEnumerable<Notification> GetNotifications()
        {
            return notificationCentre.GetAllSystemNotifications();
        }

        public void MarkNotifacationsAsRead()
        {
            notificationCentre.MarkNotificationAsRead();
        }

        public void DeleteAllNotifications()
        {
            notificationCentre.DeleteAllNotifications();
        }
    }
}