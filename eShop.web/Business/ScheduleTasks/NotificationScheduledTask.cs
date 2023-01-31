using EPiServer.Scheduler;
using eShop.web.Helpers;
using eShop.web.ViewModels;
using log4net;
using System.Reflection;

namespace eShop.web.Business.ScheduleTasks
{
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
                Message = "Schedule Task Notification"
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