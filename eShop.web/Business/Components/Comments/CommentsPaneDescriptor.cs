using EPiServer.Core;
using EPiServer.ServiceLocation;
using EPiServer.Shell;
using EPiServer.Web;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace eShop.web.Business.Components.Comments
{
    [ServiceConfiguration(typeof(IContentRepositoryDescriptor))]
    public class CommentsPaneDescriptor : ContentRepositoryDescriptorBase
    {
        public static string RepositoryKey { get { return "comments"; } }

        public override string Key { get { return RepositoryKey; } }

        public override string Name { get { return "Comments"; } }

        public override IEnumerable<Type> ContainedTypes
        {
            get
            {
                return new[]
                {
                    typeof(ContentFolder),
                    typeof(Comment)
                };
            }
        }

        public override IEnumerable<Type> CreatableTypes
        {
            get
            {
                return new[] { typeof(Comment) };
            }
        }

        public override IEnumerable<ContentReference> Roots
        {
            get
            {
                //return Enumerable.Empty<ContentReference>();
                var roots = new List<ContentReference> { SiteDefinition.Current.GlobalAssetsRoot };
                if (SiteDefinition.Current.GlobalAssetsRoot != SiteDefinition.Current.SiteAssetsRoot)
                {
                    roots.Add(SiteDefinition.Current.SiteAssetsRoot);
                }
                return roots;
            }
        }

        public override IEnumerable<Type> MainNavigationTypes
        {
            get
            {
                return new[]
                {
                    typeof(ContentFolder)
                };
            }
        }
    }
}