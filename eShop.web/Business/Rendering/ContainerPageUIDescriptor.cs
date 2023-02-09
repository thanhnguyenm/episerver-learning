using EPiServer.Shell;
using eShop.web.Models.Pages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace eShop.web.Business.Rendering
{
    [UIDescriptorRegistration]
    public class ContainerPageUIDescriptor : UIDescriptor<ContainerPage>
    {
        public ContainerPageUIDescriptor()
        {
            IconClass = "epi-iconFolder"; // or ContainerPageUIDescriptorModule
            SortKey = new EPiServer.Shell.Services.Rest.SortColumn
            {
                ColumnName = "Name",
                SortDescending = false
            };
        }
    }
}