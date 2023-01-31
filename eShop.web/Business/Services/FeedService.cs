using EPiServer;
using EPiServer.Core;
using EPiServer.DataAbstraction;
using EPiServer.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel.Syndication;
using System.Web;
using System.Web.WebPages;
using System.Xml;
using System.Xml.Linq;

namespace eShop.web.Business.Services
{
    public class FeedService : IFeedService
    {
        private readonly IPageCriteriaQueryService pageCriteriaQueryService;
        private readonly IContentTypeRepository contentTypeRepository;

        public FeedService(IPageCriteriaQueryService pageCriteriaQueryService, IContentTypeRepository contentTypeRepository)
        {
            this.pageCriteriaQueryService = pageCriteriaQueryService;
            this.contentTypeRepository = contentTypeRepository;
        }

        public SyndicationFeed Generate(string title, string url, string description)
        {
            var uri = new Uri(url);

            var feed = new SyndicationFeed()
            {
                Title = new TextSyndicationContent(title),
                Description = new TextSyndicationContent(description),
                BaseUri = uri,
                LastUpdatedTime = DateTime.Now,
                Language = "en-us",
            };

            XNamespace atom = "http://www.w3.org/2005/Atom";
            feed.AttributeExtensions.Add(new XmlQualifiedName("atom", XNamespace.Xmlns.NamespaceName), atom.NamespaceName);
            feed.ElementExtensions.Add(new XElement(atom + "link", new XAttribute("href", url), new XAttribute("rel", "self"), new XAttribute("type", "application/rss+xml")));
            feed.Links.Add(new SyndicationLink(uri, "alternate", "Link Title", "text/html", 1000));

            var formatter = feed.GetRss20Formatter();
            formatter.SerializeExtensionsAsAtom = false;

            var criterias = new PropertyCriteriaCollection
            {
                //new PropertyCriteria
                //{
                //    Condition = CompareCondition.Equal,
                //    Name = "PageTypeID",
                //    Type = PropertyDataType.PageType,
                //    Value = pageTypeId.ToString(),
                //    Required = true
                //}
                new PropertyCriteria
                {
                    Condition = EPiServer.Filters.CompareCondition.GreaterThan,
                    Name = "PageCreated",
                    Type = PropertyDataType.Date,
                    Value = DateTime.Now.AddMonths(-30).ToString(),
                    Required = true,
                }
            };

            var pages = pageCriteriaQueryService.FindPagesWithCriteria(PageReference.RootPage, criterias);
            formatter.Feed.Items = pages.Select(ConvertToSyndicationItem);


            //var pageTypeList = contentTypeRepository.List().OfType<PageType>();
            //formatter.Feed.Items = pageTypeList.Select(ConvertToSyndicationItem);

            return formatter.Feed;
        }

        //private SyndicationItem ConvertToSyndicationItem(PageType item)
        //{
        //    return new SyndicationItem
        //    {
        //        Title = new TextSyndicationContent(item.Name),
        //        Id = item.,
        //        Content = new TextSyndicationContent(item.Content),
        //        BaseUri = new Uri(item.Url),
        //        PublishDate = item.PublishDate
        //    };
        //}

        private SyndicationItem ConvertToSyndicationItem(PageData item)
        {
            return new SyndicationItem
            {
                Title = new TextSyndicationContent(item.Name),
                Id = item.ContentGuid.ToString(),
                Content = new TextSyndicationContent(item.GetPropertyValue("Description")),
                BaseUri = new Uri("http://localhost:64340" + item.LinkURL),
                PublishDate = item.Created
            };
        }
    }
}