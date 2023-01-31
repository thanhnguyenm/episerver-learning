using EPiServer.Core;
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
    public class MaxItemsAllowedAtribute : ValidationAttribute
    {
        private readonly int maxCapacity;

        public MaxItemsAllowedAtribute(int maxCapacity)
        {
            this.maxCapacity = maxCapacity;
        }

        public override bool IsValid(object value)
        {
            var contentArea = value as ContentArea;
            var isValidCount = contentArea == null
                                || contentArea.Items == null
                                || contentArea.Count <= maxCapacity;

            return isValidCount;
        }

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            var result = base.IsValid(value, validationContext);

            if (!string.IsNullOrWhiteSpace(result?.ErrorMessage))
                result.ErrorMessage = $"Content area has more than bloks than the allowed {maxCapacity}";

            return result;
        }
    }
}