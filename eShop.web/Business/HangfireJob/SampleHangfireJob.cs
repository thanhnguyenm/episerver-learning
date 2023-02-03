using Hangfire;
using System;

namespace eShop.web.Business.HangfireJob
{
    public static class SampleHangfireJob
    {
        public static void TenSecondJob()
        {
            RecurringJob.AddOrUpdate(
                    "myrecurringjob",
                    () => Console.WriteLine("Recurring!"),
                    "0/5 * * * * ?");

        }
    }
}