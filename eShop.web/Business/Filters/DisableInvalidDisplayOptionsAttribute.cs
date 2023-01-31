using EPiServer.Core;
using eShop.web.Business.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace eShop.web.Business.Filters
{
    [AttributeUsage(AttributeTargets.Property |
                    AttributeTargets.Field |
                    AttributeTargets.Parameter)]
    public class DisableInvalidDisplayOptionsAttribute : ValidationAttribute
    {
        public override bool IsValid(object value)
        {
            var contentArea = value as ContentArea;
            var noItems = contentArea?.Items == null;

            if (noItems)
                return false;

            foreach (var item in contentArea.Items)
            {
                var content = item.GetContent();

                var hasDisplayOptionLimit = content as IDisallowDisplayOption;

                if (hasDisplayOptionLimit == null)
                    continue;

                var displayOption = item.LoadDisplayOption();

                if (displayOption == null)
                    continue;

               // var optionAsEnum = GetDisplayOptionTag(displayOption.Tag);

                var containsIllegalOption = hasDisplayOptionLimit.DisabledDisplayOptions.Contains(displayOption.Tag);
                return !containsIllegalOption;
            }

            return true;
        }

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            var result = base.IsValid(value, validationContext);

            if (!string.IsNullOrWhiteSpace(result?.ErrorMessage))
            {
                result.ErrorMessage = "Block Doesn't Support This Display Opion";
            }

            return result;
        }

        //public static DisplayOptionEnum GetDisplayOptionTag(string tag)
        //{
        //    DisplayOptionEnum displayOptionEnum;
        //    Enum.TryParse(tag, out displayOptionEnum);

        //    return displayOptionEnum;
        //}
    }
}