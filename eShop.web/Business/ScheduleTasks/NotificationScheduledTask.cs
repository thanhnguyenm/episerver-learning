using EPiServer.PlugIn;
using EPiServer.Scheduler;
using eShop.web.Helpers;
using eShop.web.ViewModels;
using log4net;
using System;
using System.Reflection;
using System.Web.Hosting;

namespace eShop.web.Business.ScheduleTasks
{
    [ScheduledPlugIn(DisplayName = "Notification Scheduled Task", SortIndex = 101)]
    public class NotificationScheduledTask : ScheduledJobBase
    {
        private static readonly ILog Logger = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);

        private static bool MessagesPushed = false;

        public NotificationScheduledTask()
        {
            IsStoppable = true;
        }

        public override string Execute()
        {
            var notification = new Notification
            {
                Message = "Schedule Task Notification at " + DateTime.Now.ToLongTimeString() + $" Data: {HostingEnvironment.ApplicationPhysicalPath}"
            };


            NotificationCentre.Instance.AddNewNotification(notification);
            return ToString();
        }
        public override string ToString()
        {
            return string.Format(
            "Message pushed = {0} ",
            MessagesPushed);
        }
    }
}