using eShop.web.Business.Hubs;
using eShop.web.ViewModels;
using Microsoft.AspNet.SignalR;
using Microsoft.AspNet.SignalR.Hubs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace eShop.web.Helpers
{
    public class NotificationCentre
    {
        private static readonly Lazy<NotificationCentre> _instance = new Lazy<NotificationCentre>(() => new NotificationCentre(GlobalHost.ConnectionManager.GetHubContext<NotificationHub>().Clients));

        private readonly List<Notification> _notifications = new List<Notification>();

        private NotificationCentre(IHubConnectionContext<dynamic> clients)
        {
            Clients = clients;

            _notifications = new List<Notification>
        {
            new Notification()
        };
        }

        public static NotificationCentre Instance
        {
            get
            {
                return _instance.Value;
            }
        }

        private IHubConnectionContext<dynamic> Clients { get; set; }

        public IEnumerable<Notification> GetAllSystemNotifications()
        {
            return _notifications;
        }

        public void AddNewNotification(Notification notification)
        {
            if (notification == null || string.IsNullOrWhiteSpace(notification.Message))
                return;

            _notifications.Add(notification);
            BroacdcastUpdate();
        }

        public void MarkNotificationAsRead()
        {
            _notifications.ForEach(x => x.IsRead = true);
            BroacdcastUpdate();
        }

        public void DeleteAllNotifications()
        {
            _notifications.Clear();
            BroacdcastUpdate();
        }

        private void BroacdcastUpdate()
        {
            Clients.All.updateNotifications(_notifications);
        }
    }
}