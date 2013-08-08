using System.Collections.Generic;
using Sitecore.Common;
using Sitecore.Data.Fields;
using Sitecore.Data.Items;
using Sitecore.Events;
using System;
using System.Linq;
using SitecoreSuperchargers.GenericItemProvider.Data;

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

            var changes = GetFieldChanges(item, originalItem);

            // Now call the ISavable.Save method which will trigger custom save logic.
            var saved = savable.Save(item, originalItem, null);
            if (saved) return;

            // If there was an error saving to external system, abort save process.
            ((SitecoreEventArgs)args).Result.Cancel = true;

            // Force UI to refresh to previous item values.
            var load =
                String.Concat(new object[] { "item:load(id=", item.ID, ",language=", item.Language, ",version=", item.Version, ")" });
            Sitecore.Context.ClientPage.SendMessage(this, load);
        }

        private static bool HasChanged(Item current)
        {
            if (null == current) return false;

            var originalItem = current.Database.GetItem(current.ID, current.Language, current.Version);
            if (originalItem == null) return false;

            current.Fields.ReadAll();

            var fieldNames = current.Fields.Where(f => !f.Name.StartsWith("__")).Select(f => f.Name);
            var differences = fieldNames.Where(fieldName => current[fieldName] != originalItem[fieldName])
                                        .ToList();

            return differences.Any();
        }

        private static IEnumerable<Field> GetFieldChanges(BaseItem savable, BaseItem original)
        {
            savable.Fields.ReadAll();

            var fields = savable.Fields.ToList();
            var differences = fields.Where(f => f.Name != original[f.Name]).ToList();
            return differences.Any() ? differences : new List<Field>();
        }

        private static bool IsIgnorable(Item item)
        {
            if (item == null) return true;

            // Abort if this isn't a supported database.
            if (!Config.SupportedDatabases.Any(d => d.Equals(item.Database.Name))) return true;

            // Abort if this is a standard values item.
            if (item.Name.Equals("__Standard Values")) return true;

            // Abort if this is the IEntity template.
            if (item.TemplateID.ToGuid().ToString().Equals(IDs.TemplateIDs.IEntity)) return true;

            // Abort if this isn't an IEntity derived item.
            if (!item.IsEntity()) return true;

            // Abort if the item hasn't changed.
            return !HasChanged(item);
        }
    }
}