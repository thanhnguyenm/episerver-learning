using EPiServer.Shell;
using EPiServer.Shell.ViewComposition;
using eShop.web.Business.Components.Comments;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace eShop.web.Business.Components.Messages
{
    [Component]
    public class UserMessagePaneNavigationComponent : ComponentDefinitionBase
    {
        public UserMessagePaneNavigationComponent()
            : base("epi-cms/component/SharedBlocks")
        {
            Categories = new[] { "content" };
            Title = "User Messages";
            SortOrder = 1120;
            PlugInAreas = new[] { PlugInArea.AssetsDefaultGroup };
            Settings.Add(new Setting("repositoryKey", UserMessagesPaneDescriptor.RepositoryKey));
            WidgetType = "nitecoui/components/UserMessageComponent";
        }
    }
}