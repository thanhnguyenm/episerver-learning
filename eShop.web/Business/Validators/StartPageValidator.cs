using EPiServer.Core;
using EPiServer.Validation;
using eShop.web.Models.Blocks;
using eShop.web.Models.Pages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace eShop.web.Business.Validators
{
    public class StartPageValidator : IValidate<StartPage>
    {
        public IEnumerable<ValidationError> Validate(StartPage page)
        {
            var errors = new List<ValidationError>();

            if (page.SmallCategoryBannerContentArea != null && page.SmallCategoryBannerContentArea.Count > 4)
            {
                errors.Add(
                    new ValidationError()
                    {
                        ErrorMessage = "Too many blocks have been added",
                        PropertyName = page.GetPropertyName(p => p.SmallCategoryBannerContentArea),
                        Severity = ValidationErrorSeverity.Error,
                        ValidationType = ValidationErrorType.StorageValidation
                    }
                );
            }

            if (page.SmallCategoryBannerContentArea != null &&
                page.SmallCategoryBannerContentArea.Items.Any(x => x.GetContent().GetType().BaseType != typeof(CategoryBannerBlock)))
            {
                errors.Add(
                    new ValidationError()
                    {
                         ErrorMessage = "Category banner not correct",
                         PropertyName = page.GetPropertyName(p => p.SmallCategoryBannerContentArea),
                         Severity = ValidationErrorSeverity.Error,
                         ValidationType = ValidationErrorType.StorageValidation
                    }
                );
            }

            
            if (page.SmallCategoryBannerContentArea != null)
            {
                var options = page.SmallCategoryBannerContentArea.Items.Select(x => x.LoadDisplayOption()).ToList();
                //page.SmallCategoryBannerContentArea.FilteredItems => used FilterItems when we want to check published & permission & persionalization
                if (options.Any(x => x == null || x.Tag == Global.ContentAreaTags.OneFourthWidth))
                {
                    errors.Add(
                        new ValidationError()
                        {
                            ErrorMessage = "Category banner display option is invalid",
                            PropertyName = page.GetPropertyName(p => p.SmallCategoryBannerContentArea),
                            Severity = ValidationErrorSeverity.Error, // => ValidationErrorSeverity.Warning, ValidationErrorSeverity.Info 
                            ValidationType = ValidationErrorType.StorageValidation
                        }
                    );
                }    
                
            }
            return errors;
        }
    }
}