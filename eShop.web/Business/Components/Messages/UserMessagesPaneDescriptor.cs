using EPiServer.Core;
using EPiServer.ServiceLocation;
using EPiServer.Shell;
using EPiServer.Web;
using eShop.web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace eShop.web.Business.Components.Messages
{
    [ServiceConfiguration(typeof(IContentRepositoryDescriptor))]
    public class UserMessagesPaneDescriptor : ContentRepositoryDescriptorBase
    {
        public static string RepositoryKey { get { return "usermessage"; } }

        public override string Key { get { return RepositoryKey; } }

        public override string Name { get { return "UserMessage"; } }

        public override IEnumerable<Type> ContainedTypes
        {
            get
            {
                return new[]
                {
                    typeof(ContentFolder),
                    typeof(UserMessage)
                };
            }
        }

        public override IEnumerable<Type> CreatableTypes
        {
            get
            {
                return new[] { typeof(UserMessage) };
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