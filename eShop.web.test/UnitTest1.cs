using EPiServer;
using EPiServer.Core;
using EPiServer.DataAbstraction;
using EPiServer.ServiceLocation;
using eShop.web.Business.Services;
using eShop.web.Controllers;
using eShop.web.Models.Pages;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;

namespace eShop.web.test
{
    [TestClass]
    public class UnitTest1
    {
        [TestInitialize]
        public void Setup()
        {
            var serviceLocator = new Mock<IServiceLocator>();

            serviceLocator.Setup(x => x.GetInstance<ITestService>()).Returns(Mock.Of<ITestService>());
            serviceLocator.Setup(x => x.GetInstance<ITestService2>()).Returns(Mock.Of<ITestService2>());
            

            var contentVersionRepositoryMock = new Mock<IContentVersionRepository>();
            contentVersionRepositoryMock.Setup(x => x.List(It.IsAny<ContentReference>())).Returns(new List<ContentVersion>());

            var contentLoaderMock = new Mock<IContentLoader>();
            contentLoaderMock.Setup(x => x.GetChildren<SitePageData>(It.IsAny<ContentReference>())).Returns(new List<SitePageData>()) ;

            serviceLocator.Setup(x => x.GetInstance<IContentLoader>()).Returns(contentLoaderMock.Object);
            serviceLocator.Setup(x => x.GetInstance<IContentVersionRepository>()).Returns(contentVersionRepositoryMock.Object);
            //this.service = ServiceLocator.Current.GetInstance<ITestService>();
            //this.testService2 = ServiceLocator.Current.GetInstance<ITestService2>();
            //this.contentVersionRepository = ServiceLocator.Current.GetInstance<IContentVersionRepository>();
            //this.contentLoader = ServiceLocator.Current.GetInstance<IContentLoader>();

            ServiceLocator.SetLocator(serviceLocator.Object);
        }

        [TestMethod]
        public void TestMethod1()
        {
            var currentPage = new StartPage
            {
                ContentLink = new ContentReference (1),
            };

            var controller = new StartPageController();
            var result = controller.Index(currentPage);

            Assert.IsNotNull(result);
        }
    }
}
