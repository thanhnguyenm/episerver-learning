using EPiServer;
using EPiServer.Core;
using EPiServer.DataAbstraction;
using EPiServer.DataAnnotations;
using eShop.web.ViewModels;
using System.ComponentModel.DataAnnotations;

namespace eShop.web.Models.Blocks
{
    [ContentType(
        DisplayName = "Login Control Block", 
        GUID = "3452515d-8188-4c09-b3de-00ce8e789c48", 
        Description = "The login Form data")]
    [BlockSiteImageUrl]
    public class LoginControlBlock : SiteBlockData
    {
        [Ignore]
        public LoginModel LoginModel { get; set; }

        [CultureSpecific]
        [Display(Name = "Block Name",
            GroupName = SystemTabNames.Content,
            Order = 100)]
        [Required]
        public virtual string BlockName { get; set; }

        [CultureSpecific]
        [Display(Name = "Block Description",
            GroupName = SystemTabNames.Content,
            Order = 200)]
        [Required]
        public virtual XhtmlString BlockDescription { get; set; }

        [CultureSpecific]
        [Display(Name = "User Name Label",
            GroupName = SystemTabNames.Content,
            Order = 300)]
        [Required]
        public virtual string UserNameLabel { get; set; }

        [CultureSpecific]
        [Display(Name = "User Name Placeholder",
            GroupName = SystemTabNames.Content,
            Order = 400)]
        [Required]
        public virtual string UserNamePlaceholder { get; set; }

        [CultureSpecific]
        [Display(Name = "Password Label",
            GroupName = SystemTabNames.Content,
            Order = 500)]
        [Required]
        public virtual string PasswordLabel { get; set; }

        [CultureSpecific]
        [Display(Name = "Forgot Your Password Text",
            GroupName = SystemTabNames.Content,
            Order = 700)]
        [Required]
        public virtual string ForgotYourPasswordText { get; set; }

        [CultureSpecific]
        [Display(Name = "Forgot Your Password Url",
            GroupName = SystemTabNames.Content,
            Order = 800)]
        [Required]
        public virtual Url ForgotYourPasswordUrl { get; set; }

        [CultureSpecific]
        [Display(Name = "Remember Me Text",
            GroupName = SystemTabNames.Content,
            Order = 900)]
        [Required]
        public virtual string RememberMeText { get; set; }

        [CultureSpecific]
        [Display(Name = "Sign-In Text",
            GroupName = SystemTabNames.Content,
            Order = 1000)]
        [Required]
        public virtual string SignInText { get; set; }

        [CultureSpecific]
        [Display(Name = "Sign-In Form Url",
            GroupName = SystemTabNames.Content,
            Order = 1100)]
        [Required]
        public virtual Url SignInFormUrl { get; set; }
    }
}