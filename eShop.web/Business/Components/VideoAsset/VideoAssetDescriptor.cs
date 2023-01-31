using EPiServer.Cms.Shell.UI.UIDescriptors;
using EPiServer.Core;
using EPiServer.ServiceLocation;
using EPiServer.Shell;
using EPiServer.Web;
using eShop.web.Models.Media;
using System;
using System.Collections.Generic;

namespace eShop.web.Business.Components.VideoAsset
{
    [ServiceConfiguration(typeof(IContentRepositoryDescriptor))]
    public class VideoAssetDescriptor : MediaRepositoryDescriptor
    {
        public new static string RepositoryKey => "video";

        public override string Key => RepositoryKey;

        public override string Name => "video";

        public override IEnumerable<Type> ContainedTypes => new[]
        {
            typeof(ContentFolder),
            typeof(VideoFile)
        };

        public override IEnumerable<Type> CreatableTypes => new[] { typeof(VideoFile) };

        public override IEnumerable<ContentReference> Roots
        {
            get
            {
                var roots = new List<ContentReference> { SiteDefinition.Current.GlobalAssetsRoot };
                if (SiteDefinition.Current.GlobalAssetsRoot != SiteDefinition.Current.SiteAssetsRoot)
                {
                    roots.Add(SiteDefinition.Current.SiteAssetsRoot);
                }
                return roots;
            }
        }

        public override IEnumerable<Type> MainNavigationTypes => new[]
        {
            typeof(ContentFolder)
        };
    }
}