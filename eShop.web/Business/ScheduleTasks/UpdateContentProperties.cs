using EPiServer.PlugIn;
using EPiServer.Scheduler;
using log4net;
using Mediachase.BusinessFoundation.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web;

namespace eShop.web.Business.ScheduleTasks
{
    [ScheduledPlugIn(DisplayName = "Update Content Properties Task", SortIndex = 102)]
    public class UpdateContentProperties : ScheduledJobBase
    {
        private static readonly ILog Logger = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);


        public UpdateContentProperties()
        {
            IsStoppable = true;
        }

        public override string Execute()
        {
            //var fashionProductClass = DataContext.Current.GetMetaClass("FashionProduct");
            //fashionProductClass.DeleteMetaField("Size");

            var metaclass = DataContext.Current.MetaModel.MetaClasses;


            return ToString();
        }
        public override string ToString()
        {
            return string.Format("UpdateContentProperties  job completed");
        }
    }
}