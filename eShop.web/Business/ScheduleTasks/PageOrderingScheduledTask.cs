using EPiServer;
using EPiServer.Core;
using EPiServer.DataAbstraction;
using EPiServer.PlugIn;
using EPiServer.Scheduler;
using EPiServer.ServiceLocation;
using eShop.web.Models.Pages;
using log4net;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Web;

namespace eShop.web.Business.ScheduleTasks
{
    [ScheduledPlugIn(DisplayName = "Page Ordering Scheduled Task", SortIndex = 103)]
    public class PageOrderingScheduledTask : ScheduledJobBase
    {
        private static readonly ILog Logger = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);

        int contentProcessed;

        int contentNotProcessed;

        int index;

        public PageOrderingScheduledTask()
        {
            IsStoppable = true;
            contentProcessed = 0;
            contentNotProcessed = 0;
            index = 0;
        }

        private long Duration { get; set; }

        public override string Execute()
        {
            var tmr = Stopwatch.StartNew();

            var contentTypeRepository = ServiceLocator.Current.GetInstance<IContentTypeRepository>();
            var contentRepository = ServiceLocator.Current.GetInstance<IContentRepository>();
            var contentModelUsage = ServiceLocator.Current.GetInstance<IContentModelUsage>();

            try
            {
                var contentType = contentTypeRepository.Load<StandardPage>();
                var usages = contentModelUsage.ListContentOfContentType(contentType).OrderBy(x => x.Name);

                foreach (var content in usages)
                {
                    var page = contentRepository.Get<PageData>(content.ContentLink);

                    if (page == null)
                        break;

                    index++;

                    var clone = page.CreateWritableClone();
                    clone.Property["PagePeerOrder"].Value = index;
                    contentRepository.Save(clone, EPiServer.DataAccess.SaveAction.Publish);

                    contentProcessed++;
                }
            }
            catch (Exception ex)
            {
                contentNotProcessed = contentNotProcessed++;
                Logger.Error(ex);
            }

            tmr.Stop();
            Duration = tmr.ElapsedMilliseconds;

            return ToString();
        }

        public override string ToString()
        {
            var logMesssage = contentNotProcessed > 0 ? "Please check the logs for the failed pages." : string.Empty;

            return string.Format(
                "Processed {0} in {1}ms on {2}. {3}",
                contentProcessed,
                Duration,
                Environment.MachineName,
                logMesssage);
        }
    }
}