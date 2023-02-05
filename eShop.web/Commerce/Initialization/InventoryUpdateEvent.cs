using EPiServer.Events;
using EPiServer.Events.Clients;
using EPiServer.Framework;
using EPiServer.Framework.Initialization;
using EPiServer.ServiceLocation;
using eShop.web.Helpers;
using Mediachase.Commerce.Engine.Events;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Web;

namespace eShop.web.Commerce.Initialization
{
    [InitializableModule]
    [ModuleDependency(typeof(EPiServer.Commerce.Initialization.InitializationModule))]
    public class InventoryUpdateEvent : IInitializableModule
    {
        public void Initialize(InitializationEngine context)
        {
            AddEvent();
        }

        public void Preload(string[] parameters)
        {
        }

        public void Uninitialize(InitializationEngine context)
        {
        }

        public void AddEvent()
        {
            Event ev = Event.Get(CatalogKeyEventBroadcaster.CatalogKeyEventGuid);
            ev.Raised += CatalogKeyEventUpdated;
        }

        private void CatalogKeyEventUpdated(object sender, EventNotificationEventArgs e)
        {
            var eventArgs = (CatalogKeyEventArgs)DeSerialize((byte[])e.Param);
            var inventoryUpdatedEventArgs = eventArgs as InventoryUpdateEventArgs;
            if (inventoryUpdatedEventArgs != null)
            {
                RemoteInventoryUpdated(sender, inventoryUpdatedEventArgs);
            }
        }

        private void RemoteInventoryUpdated(object sender, InventoryUpdateEventArgs inventoryUpdatedEventArgs)
        {
            var productKeys = string.Join(",", inventoryUpdatedEventArgs.CatalogKeys.Select(x => x.CatalogEntryCode));
            //Your action when inventories are updated remotely.
            NotificationCentre.Instance.AddNewNotification(new ViewModels.Notification
            {
                Message = $"inventory of {productKeys} changed",
                Date = DateTime.Now,
                IsRead = false
            });
        }

        protected virtual CatalogKeyEventArgs DeSerialize(byte[] buffer)
        {
            var formatter = new BinaryFormatter();
            var stream = new MemoryStream(buffer);
            return formatter.Deserialize(stream) as CatalogKeyEventArgs;
        }
    }
}