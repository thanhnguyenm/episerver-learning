using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace eShop.web.Business.Services
{
    public class TestService : ITestService
    {
        public void Hello()
        {
            Console.WriteLine("Test");
        }
    }
}