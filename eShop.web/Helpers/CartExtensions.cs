using EPiServer.Commerce.Order;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace eShop.web.Helpers
{
    public static class CartExtensions
    {
        public static bool HasItemBeenRemoved(this IDictionary<ILineItem, IList<ValidationIssue>> issuesPerLineItem, ILineItem lineItem)
        {
            IList<ValidationIssue> issues;
            if (issuesPerLineItem.TryGetValue(lineItem, out issues))
            {
                return issues.Any(x => x == ValidationIssue.RemovedDueToInactiveWarehouse ||
                        x == ValidationIssue.RemovedDueToCodeMissing ||
                        x == ValidationIssue.RemovedDueToInsufficientQuantityInInventory ||
                        x == ValidationIssue.RemovedDueToInvalidPrice ||
                        x == ValidationIssue.RemovedDueToMissingInventoryInformation ||
                        x == ValidationIssue.RemovedDueToNotAvailableInMarket ||
                        x == ValidationIssue.RemovedDueToUnavailableCatalog ||
                        x == ValidationIssue.RemovedDueToUnavailableItem);
            }
            return false;
        }
    }
}