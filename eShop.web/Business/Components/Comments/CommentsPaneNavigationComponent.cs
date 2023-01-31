using EPiServer.Shell;
using EPiServer.Shell.ViewComposition;

namespace eShop.web.Business.Components.Comments
{
    [Component]
    public class CommentsPaneNavigationComponent : ComponentDefinitionBase
    {
        public CommentsPaneNavigationComponent()
            : base("epi-cms/component/SharedBlocks")
        {
            Categories = new[] { "content" };
            Title = "Comments";
            SortOrder = 1000;
            PlugInAreas = new[] { PlugInArea.AssetsDefaultGroup };
            Settings.Add(new Setting("repositoryKey", CommentsPaneDescriptor.RepositoryKey));
            WidgetType = "nitecoui/components/CommentComponent";
        }
    }
}