using EPiServer.Core;
using EPiServer.Validation;
using eShop.web.Models.Blocks;
using eShop.web.Models.Pages;
using System.Collections.Generic;
using System.Linq;

namespace eShop.web.Business.Validators
{
    public class CategoryBannerTypeVaidator : IValidate<StartPage>
    {
        public IEnumerable<ValidationError> Validate(StartPage page)
        {
            //if (page.SmallCategoryBannerContentArea != null &&
            //    page.SmallCategoryBannerContentArea.Items.Any(x => x.GetContent().GetType().BaseType != typeof(CategoryBannerBlock)))
            //{                
            //    return new ValidationError[]
            //    {
            //        new ValidationError()
            //        {
            //             ErrorMessage = "Category banner not correct",
            //             PropertyName = page.GetPropertyName(p => p.SmallCategoryBannerContentArea),
            //             Severity = ValidationErrorSeverity.Error,
            //             ValidationType = ValidationErrorType.StorageValidation
            //        }
            //    };
            //}

            return Enumerable.Empty<ValidationError>();
        }
    }
}