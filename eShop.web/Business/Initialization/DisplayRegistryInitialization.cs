using EPiServer.Framework;
using EPiServer.Framework.Initialization;
using EPiServer.ServiceLocation;
using EPiServer.Web;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace eShop.web.Business.Initialization
{
    [InitializableModule]
    [ModuleDependency(typeof(EPiServer.Web.InitializationModule))]
    public class DisplayRegistryInitialization : IInitializableModule
    {
        public void Initialize(InitializationEngine context)
        {
            if (context.HostType == HostType.WebApplication)
            {
                // Register Display Options
                var options = ServiceLocator.Current.GetInstance<DisplayOptions>();
                options
                    .Add("full", "/displayoptions/full", Global.ContentAreaTags.FullWidth, string.Empty, "epi-icon__layout--full")
                    .Add("wide", "/displayoptions/wide", Global.ContentAreaTags.TwoThirdsWidth, string.Empty, "epi-icon__layout--two-thirds")
                    .Add("half", "/displayoptions/half", Global.ContentAreaTags.HalfWidth, string.Empty, "epi-icon__layout--half")
                    .Add("narrow", "/displayoptions/narrow", Global.ContentAreaTags.OneThirdWidth, string.Empty, "epi-icon__layout--one-third")
                    .Add("quarter", "/displayoptions/quarter", Global.ContentAreaTags.OneFourthWidth, string.Empty, "epi-icon__layout--one-fourth");

            }
        }

        public void Preload(string[] parameters) { }

        public void Uninitialize(InitializationEngine context) { }
    }
}