using EPiServer.PlugIn;
using EPiServer.ServiceLocation;
using eShop.web.Templates.Plugins;
using System;
using System.Data;

namespace eShop.web.Business.Services
{
    [ServiceConfiguration(ServiceType = typeof(IExamplePluginService), Lifecycle = ServiceInstanceScope.HttpContext)]
    public class ExamplePluginService : IExamplePluginService
    {
        private DataSet customDataSet;

        private string Key = "ExamplePlugin";

        private static readonly log4net.ILog Logger = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        public ExamplePluginService()
        {
            customDataSet = new DataSet();
            customDataSet.Tables.Add(new DataTable());
            customDataSet.Tables[0].Columns.Add(new DataColumn(Key, typeof(string)));
        }
        public string LoadData()
        {
            var returnValue = string.Empty;

            try
            {
                PlugInSettings.Populate(typeof(ExamplePlugin), customDataSet);

                var lastRowNo = customDataSet.Tables[0].Rows.Count;
                returnValue = customDataSet.Tables[0].Rows[lastRowNo - 1][Key].ToString();
            }
            catch(Exception ex)
            {
                Logger.Error(ex);
            }

            return returnValue;
        }

        public void SaveData(string value)
        {
            try
            {
                var newRow = customDataSet.Tables[0].NewRow();
                newRow[Key] = value;
                customDataSet.Tables[0].Rows.Add(newRow);

                PlugInSettings.Save(typeof(ExamplePlugin), customDataSet);
            }
            catch(Exception ex)
            {
                Logger.Error(ex);
            }
        }
    }
}