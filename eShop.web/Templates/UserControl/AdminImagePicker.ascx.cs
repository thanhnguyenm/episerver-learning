using EPiServer;
using EPiServer.ClientScript;
using System;

namespace eShop.web.Templates.UserControl
{
    // This guideline for EPi6, not work with Epi 11
    public partial class AdminImagePicker : System.Web.UI.UserControl
    {
        /// <summary>
        /// Code for document browsing
        /// </summary>
        public bool DisplayImagesOnly { get; set; }

        /// <summary>
        /// Returns the path to the selected file
        /// </summary>
        public string FilePath
        {
            get
            {
                return tbFilePath.Text;
            }
            set { tbFilePath.Text = value; }
        }

        public AdminImagePicker()
        {
            DisplayImagesOnly = false;
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            SetupControl();
        }

        /// <summary>
        /// Setup control and include necessary scripts
        /// </summary>
        private void SetupControl()
        {
            submitButton.Attributes.Add("onclick", string.Format("OpenFileDialog('{0}', {1})", tbFilePath.ClientID, DisplayImagesOnly.ToString().ToLower()));

            ClientScriptUtility.RegisterClientScriptFile(Page, UriSupport.ResolveUrlFromUtilBySettings("javascript/episerverscriptmanager.js"));
            ClientScriptUtility.RegisterClientScriptFile(Page, UriSupport.ResolveUrlFromUIBySettings("javascript/system.js"));
            ClientScriptUtility.RegisterClientScriptFile(Page, UriSupport.ResolveUrlFromUIBySettings("javascript/dialog.js"));
        }
    }
}