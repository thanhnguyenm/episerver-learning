using EPiServer.Shell;
using EPiServer.Shell.ViewComposition;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace eShop.web.Business.Components.VideoAsset
{
    [Component]
    public class VideoAssetComponent : ComponentDefinitionBase
    {
        public VideoAssetComponent()
            : base("epi-cms/component/SharedBlocks")
        {
            PlugInAreas = new[] { PlugInArea.AssetsDefaultGroup }; // Which area this component be rendered in.
            Categories = new[] { "content" }; // Where to display the widget (cms / commerce / find / content / dashboard ).
            WidgetType = "nitecoui/components/VideoAsset"; // Tell EPiServer where to find the custom JS file that will define which view should be loaded.
            SortOrder = 1100;
            Description = "Video asset to store video content type";
            Title = "Video Asset";
            Settings.Add(new Setting("repositoryKey", VideoAssetDescriptor.RepositoryKey));
        }
    }
}