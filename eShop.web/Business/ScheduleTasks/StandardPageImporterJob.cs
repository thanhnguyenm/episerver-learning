using EPiServer;
using EPiServer.Core;
using EPiServer.DataAccess;
using EPiServer.PlugIn;
using EPiServer.Scheduler;
using EPiServer.Security;
using EPiServer.ServiceLocation;
using eShop.web.Helpers;
using eShop.web.Models.Pages;
using log4net;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;

namespace eShop.web.Business.ScheduleTasks
{
    [ScheduledPlugIn(DisplayName = "Content Page Importer", SortIndex = 103)]
    public class StandardPageImporterJob : ScheduledJobBase
    {
        private static readonly ILog Logger = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);
        private readonly IContentRepository contentRepository;
        private int processedNodes;

        private int failedNodes;

        public StandardPageImporterJob(IContentRepository contentRepository)
        {
            processedNodes = 0;
            failedNodes = 0;
            this.contentRepository = contentRepository;
        }

        private long Duration { get; set; }

        public override string Execute()
        {
            var timer = Stopwatch.StartNew();

            var fileDirectory = FileHelper.GetImportDirectoryPath();

            var jsonFiles = FileHelper.GetFiles(fileDirectory);

            if (jsonFiles.Any())
            {
                ProcessFiles(jsonFiles);
            }
            else
                return "No files to process";

            timer.Stop();
            Duration = timer.ElapsedMilliseconds;

            return ToString();
        }

        private void ProcessFiles(List<string> jsonFiles)
        {
            foreach (var jsonFile in jsonFiles)
            {
                using (var streamReader = new StreamReader(jsonFile))
                {
                    var json = streamReader.ReadToEnd();

                    StandardPage contentPageData;

                    var settings = new JsonSerializerSettings
                    {
                        NullValueHandling = NullValueHandling.Ignore
                    };

                    try
                    {
                        contentPageData = JsonConvert.DeserializeObject<StandardPage>(json, settings);
                    }
                    catch (JsonSerializationException ex)
                    {
                        Logger.Error(string.Format("Invalid Json file {0}", jsonFile), ex);
                        failedNodes = failedNodes + 1;
                        continue;
                    }
                    catch (JsonReaderException ex)
                    {
                        Logger.Error(string.Format("Invalid Json format within {0}", jsonFile), ex);
                        failedNodes = failedNodes + 1;
                        continue;
                    }

                    contentPageData.ParentLink = ContentReference.RootPage;

                    var contentPageReference = CreateContentPage(contentPageData);

                    if (contentPageReference == null)
                    {
                        Logger.ErrorFormat("Unable to get create blog page {0} ", contentPageData.PageName);
                        failedNodes = failedNodes + 1;
                        continue;
                    }

                    processedNodes = processedNodes + 1;
                }
            }
        }

        private StandardPage CreateContentPage(StandardPage contentPageData)
        {
            var existingPage = contentRepository
                                                .GetChildren<StandardPage>(ContentReference.RootPage)
                                                .FirstOrDefault(x => x.PageName == contentPageData.PageName);

            if (existingPage != null)
                return existingPage;

            var newPage = contentRepository
                       .GetDefault<StandardPage>(contentPageData.ParentLink);

            newPage.PageName = contentPageData.PageName;
            newPage.MetaTitle = contentPageData.PageName;
            newPage.Name = contentPageData.PageName;

            newPage.MetaDescription = contentPageData.MetaDescription;
            newPage.MetaKeywords = contentPageData.MetaKeywords;

            return Save(newPage) != null ? newPage : null;
        }

        public ContentReference Save(StandardPage contentPage,
                                     SaveAction saveAction = SaveAction.Publish,
                                     AccessLevel accessLevel = AccessLevel.NoAccess)
        {
            if (contentPage == null)
                return null;

            return contentRepository.Save(contentPage, saveAction, accessLevel);
        }

        public override string ToString()
        {
            return string.Format(
                "Imported {0} pages successfully in {1}ms on. {2} page(s) failed to import.",
                processedNodes,
                Duration,
                failedNodes);
        }
    }
}