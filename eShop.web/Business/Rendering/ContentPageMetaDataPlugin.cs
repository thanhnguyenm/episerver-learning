using EPiServer.Core;
using EPiServer.ServiceLocation;
using EPiServer.Shell;
using eShop.web.Models.Pages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace eShop.web.Business.Rendering
{
    [ServiceConfiguration(typeof(EPiServer.Shell.ViewConfiguration))]
    public class ContentPageMetaDataPlugin : ViewConfiguration<PageData>
    {
        public ContentPageMetaDataPlugin()
        {
            Key = "ContentPageMetaDataPlugin";
            Name = "Page Debugging Information";
            Description = "Page Debugging Information";
            ControllerType = "epi-cms/widget/IFrameController";
            ViewType = "/DebuggingInformation/";
            IconClass = "epi-iconForms";
        }
    }
}