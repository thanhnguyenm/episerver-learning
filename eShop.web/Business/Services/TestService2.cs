using EPiServer.ServiceLocation;
using System;

namespace eShop.web.Business.Services
{
    [ServiceConfiguration(ServiceType = typeof(ITestService2), Lifecycle = ServiceInstanceScope.HttpContext)]
    public class TestService2 : ITestService2
    {
        public void Hello()
        {
            Console.WriteLine("Test");
        }
    }
}