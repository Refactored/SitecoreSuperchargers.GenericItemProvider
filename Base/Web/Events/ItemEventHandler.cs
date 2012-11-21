using Sitecore;
using Sitecore.Data.Items;
using Sitecore.Events;
using SitecoreSuperchargers.GenericItemProvider.Data;
using System;
using System.Linq;

namespace SitecoreSuperchargers.GenericItemProvider.Events
{
    public class ItemEventHandler
    {
        public void OnItemSaving(object sender, EventArgs args)
        {
            var item = Event.ExtractParameter(args, 0) as Item;
            if (null == item) return;
            if (IsIgnorable(item)) return;

            // Make sure this is an ISavable entity before continuing.
            var savable = Helpers.EntityHelper.CreateSavableInstance(item);
            if (savable == null) return;

            // Get a copy of the original item to pass to the Save method.
            var originalItem = item.Database.GetItem(item.ID, item.Language, item.Version);

            // --- Get field changes which is passed to the Save method.
            var changes = Helpers.ItemHelper.GetFieldChanges(item, originalItem);

            // Now call the ISavable.Save method which will trigger custom save logic.
            var saved = savable.Save(item, originalItem, changes);
            if (saved) return;

            // External data store save failed, abort save process.
            ((SitecoreEventArgs) args).Result.Cancel = true;

            // Force UI to refresh to previous item values.
            var load =
                String.Concat(new object[]
                    {"item:load(id=", item.ID, ",language=", item.Language, ",version=", item.Version, ")"});
            Context.ClientPage.SendMessage(this, load);
        }

        private static bool IsIgnorable(Item item)
        {
            if (item == null) return true;

            // Abort if this isn't a supported database.
            if (!Config.SupportedDatabases.Any(d => d.Equals(item.Database.Name))) return true;

            // Abort if this is a standard values item.
            if (item.Name.Equals(Constants.StandardValuesItemName)) return true;

            // Abort if this is the IEntity template.
            if (item.TemplateID.ToGuid().ToString().Equals(IDs.TemplateIDs.IEntity)) return true;

            // Abort if this isn't an IEntity derived item.
            if (!item.IsEntity()) return true;

            // Abort if the item hasn't changed.
            return !Helpers.ItemHelper.HasChanged(item);
        }
    }
}