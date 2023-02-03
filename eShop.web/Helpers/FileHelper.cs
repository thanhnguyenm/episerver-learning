using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Hosting;

namespace eShop.web.Helpers
{
    public static class FileHelper
    {
        public static List<string> GetFiles(string directory)
        {
            var files = new List<string>();

            files.AddRange(Directory.GetFiles(directory, "*.json", SearchOption.TopDirectoryOnly));

            return files;
        }

        public static string GetImportDirectoryPath()
        {
            var webRoot = new DirectoryInfo(HostingEnvironment.ApplicationPhysicalPath);


            return string.Format("{0}Import", webRoot);
        }
    }
}