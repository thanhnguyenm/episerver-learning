using EPiServer.PlugIn;
using EPiServer.Scheduler;
using log4net;
using System;
using System.Diagnostics;
using System.Reflection;

namespace eShop.web.Business.Jobs
{
    [ScheduledPlugIn(DisplayName = "Bulk Content Import", SortIndex = 100)]
    public class ConsoleLogJob : ScheduledJobBase
    {
        private static readonly ILog Logger = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);

        public ConsoleLogJob()
        {
            IsStoppable = true;
        }

        private long Duration { get; set; }

        public override string Execute()
        {
            var tmr = Stopwatch.StartNew();

            try
            {
                // Do Work here
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
            }

            tmr.Stop();
            Duration = tmr.ElapsedMilliseconds;

            return ToString();
        }
    }
}