using EPiServer;
using EPiServer.PlugIn;
using EPiServer.Security;
using eShop.web.Business.Services;
using System;

namespace eShop.web.Templates.Plugins
{
    [GuiPlugIn(DisplayName = "Example Plugin",
        Description = "Example",
        Area = PlugInArea.AdminMenu,
        Url = "~/Templates/Plugins/ExamplePlugin.aspx")]
    public partial class ExamplePlugin : SimplePage
    {
        private readonly IExamplePluginService examplePluginService;

        // TODO: Still not inject service into aspx page
        public ExamplePlugin()
        {
            this.examplePluginService = new ExamplePluginService();
        }

        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);

            if (!PrincipalInfo.HasAdminAccess)
            {
                AccessDenied();
            }
        }

        public string GetTheTime()
        {
            return examplePluginService.LoadData();
        }

        protected void UpdateTheTime(object sender, EventArgs e)
        {
            var time = DateTime.Now.ToString("HH:MM:ss");
            examplePluginService.SaveData(time);
        }
    }
}