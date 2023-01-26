// Some Epi server api https://www.jondjones.com/learn-episerver-cms/episerver-developers-tutorials/episerver-api-explained/

            // Get start page referance.
            var contentLoader = ServiceLocator.Current.GetInstance<IContentLoader>();
            var startpage = contentLoader.Get<PageData>(ContentReference.StartPage);
            var contentLink = startpage.ContentLink;

            // Get paage ref outside controller
            //var pageRouteHelper = EPiServer.ServiceLocation.ServiceLocator.Current.GetInstance<EPiServer.Web.Routing.PageRouteHelper>();
            //var pageReference = pageRouteHelper.PageLink;

            //var pageRouteHelper = EPiServer.ServiceLocation.ServiceLocator.Current.GetInstance<EPiServer.Web.Routing.PageRouteHelper>();
            //var currentPage = pageRouteHelper.Page;


            //Get start page, root page, block container
            //EPiServer.Core.ContentReference.StartPage

            //EPiServer.Core.ContentReference.RootPage

            //EPiServer.Core.ContentReference.SiteBlockFolder


            //Get Link with language
            //var swedishLink = EPiServer.UriSupport.AddLanguageSelection(page.LinkURL, "sv");
            //var swedishLink = EPiServer.UriSupport.AddLanguageSelection(page.LinkURL, page.LanguageID);


            // Search criteria

            // searc by pagetyeid
            //        var pageTypeId = ServiceLocator.Current.GetInstance<ContentTypeRepository>()
            //.Load<StartPage>()
            //.ID;

            //        var criteria = new PropertyCriteria
            //        {
            //            Condition = CompareCondition.Equal,
            //            Name = "PageTypeID",
            //            Type = PropertyDataType.PageType,
            //            Value = pageTypeId,
            //            Required = true
            //        };


            //Search by page name
            //new PropertyCriteria()
            //{
            //    Name = "PageName",
            //    Type = PropertyDataType.String,
            //    Condition = EPiServer.Filters.CompareCondition.Equal,
            //    Value = "Search Term"
            //}

            // Search by created-date
            //var criteria = new PropertyCriteria
            //{
            //    Condition = EPiServer.Filters.CompareCondition.GreaterThan,
            //    Name = "PageCreated",
            //    Type = PropertyDataType.Date,
            //    Value = DateTime.Now.AddMonths(-30).ToString(),
            //    Required = true,
            //};

            // search action and result
            //var criterias = new PropertyCriteriaCollection
            //{
            //    new PropertyCriteria()
            //    {
            //        Name = "PageName",
            //        Type = PropertyDataType.String,
            //        Condition = EPiServer.Filters.CompareCondition.Equal,
            //        Value = _searchTerm
            //    }
            //};

            //var repository = ServiceLocator.Current.GetInstance<IPageCriteriaQueryService>(); ==> only search published pages and directly from DB +> performance care

            //var pages = repository.FindPagesWithCriteria(
            //    PageReference.StartPage,
            //    criterias);


            // FINDALLPAGESWITHCRITERIA => seach both publish and unpublish, not check permission=


            // Check request mode
            //EPiServer.Web.Routing.Segments.RequestSegmentContext.CurrentContextMode  ==> Preview mode
            //var inEditMode = PageEditing.PageIsInEditMode; ==> true => edit mode => get URL edit mode var pagesEditorUrl = PageEditing.GetEditUrl(new ContentReference(3));


            // get all content-type
            //var contentTypeRepository = ServiceLocator.Current.GetInstance<IContentTypeRepository>();
            //var pageTypeList = contentTypeRepository.List().OfType<PageType>();


            // get objects of a content type
            //var contentModelUsage = ServiceLocator.Current.GetInstance<IContentModelUsage>();
            //var contentTypeRepository = ServiceLocator.Current.GetInstance<IContentTypeRepository>();

            //var contentType = contentTypeRepository.Load<ProductPage>();
            //var usages = contentModelUsage.ListContentOfContentType(contentType).OrderBy(x => x.Name);


            // get App_data folder 
            //< appData basePath = "App_Data" />
            // var appDataBasePath = EPiServer.Framework.Configuration.EPiServerFrameworkSection.Instance.AppData.BasePath;

            //if (appDataBasePath.ToLower() == "app_data")
            //{
            //    addDataBasePath = $@"{AppDomain.CurrentDomain.BaseDirectory}{appDataBasePath}";

            //}


            //CHeck Ispublished content
            //public bool IsContentPublished(IContent content)
            //{
            //    var repository = ServiceLocator.Current.GetInstance<IPublishedStateAssessor>();
            //    return repository.IsPublished(content, PagePublishedStatus.Published);
            //}

            //get URL editing mode
            //var reference = new ContentReference(3);
            //var url = PageEdititng.GetEditUrl(reference)

    ////or using UrlResolver ==> internal URL
    var urlResolver = ServiceLocator.Current.GetInstance<UrlResolver>();
    //var reference = new ContentReference(3);
    //var reference = startpage.ContentLink;
    //var arguments = new VirtualPathArguments
    //{
    //    ContextMode = ContextMode.Edit
    //};
    //var langauage = "en";
    //var ursl = urlResolver.GetUrl(reference, langauage, arguments);

    // Projects
    //var projectRepository = ServiceLocator.Current.GetInstance<ProjectRepository>();
    //var allProjects = projectRepository.List();



------------some useful sql queries

select sysobjects.Name, syscolumns.Name from sysobjects inner join syscolumns on sysobjects.id = syscolumns.id where sysobjects.xtype = 'U' order by sysobjects.Name, syscolumns.colorder


SELECT t.NAME AS TableName, s.Name AS SchemaName, p.rows AS RowCounts, SUM(a.total_pages) 
* 8 AS TotalSpaceKB, SUM(a.used_pages) * 8 AS UsedSpaceKB, (SUM(a.total_pages) - SUM(a.used_pages))
* 8 AS UnusedSpaceKB FROM sys.tables t INNER JOIN sys.indexes i ON t.OBJECT_ID = i.object_id 
INNER JOIN sys.partitions p ON i.object_id = p.OBJECT_ID AND i.index_id = p.index_id 
INNER JOIN sys.allocation_units a ON p.partition_id = a.container_id LEFT 
OUTER JOIN sys.schemas s ON t.schema_id = s.schema_id WHERE t.NAME 
NOT LIKE 'dt%' AND t.is_ms_shipped = 0 AND i.OBJECT_ID > 255 GROUP BY t.Name, s.Name, p.Rows ORDER BY t.Name


Select * From tblContentType


select ct.Name 'Page Type Name', pd.Name 'Property Name' From [dbo].[tblContentType]
ct Inner Join [dbo].[tblPropertyDefinition] pd on ct.pkID = pd.fkContentTypeID 
Order by ct.Name, pd.FieldOrder 

--
Beta feature ???????

---
JON'S 5 PRINCIPLES OF BLOCK DESIGN
All of my ERpiserver content modelling adivce can be boiled down to these rules:

On the homepage, be block focused

Avoid nesting content areas within content areas

Try to follow the DRY principle

Favour creating a new block over creating a new page-type

Choose configuration overduplication

----
Configuration Over Duplication When Designing Your Episerver Website

-One Component Per UI Design
Or
-A Configurable Block