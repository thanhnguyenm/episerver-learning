//using EPiServer.Framework;
//using EPiServer.Framework.Initialization;
//using EPiServer.Shell;
//using eShop.web.Models.Pages;
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Reflection;
//using System.Web;

//namespace eShop.web.Business.Initialization
//{
//    [InitializableModule]
//    [ModuleDependency(typeof(EPiServer.Web.InitializationModule))]
//    public class ContainerPageUIDescriptorModule : IInitializableModule
//    {
//        public void Initialize(InitializationEngine context)
//        {
//            SetIcons(context.Locate.Advanced.GetInstance<UIDescriptorRegistry>());
//        }

//        private static void SetIcons(UIDescriptorRegistry uiDescriptorRegistry)
//        {
//            var instances =
//                Assembly.GetExecutingAssembly()
//                    .GetTypes()
//                    .Where(t => t.GetInterfaces().Contains(typeof(IContainerPage)));

//            var descriptors = uiDescriptorRegistry.UIDescriptors;

//            foreach (var instance in instances)
//            {
//                var descriptor = descriptors.FirstOrDefault(x => x.ForType.FullName == instance.ToString());

//                if (descriptor != null)
//                    descriptor.IconClass = "epi-iconFolder";

//            }
//        }

//        public void Preload(string[] parameters)
//        {
//        }

//        public void Uninitialize(InitializationEngine context)
//        {
//        }
//    }
//}