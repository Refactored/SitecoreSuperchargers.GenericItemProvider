using Sitecore.Data.Items;
using Sitecore.Events;
using System;
using System.Linq;

namespace SitecoreSuperchargers.GenericItemProvider.Events
{
    public class ItemEventHandler
    {
        public void OnItemSaving(object sender, EventArgs args)
        {
            var item = Event.ExtractParameter(args, 0) as Item;
            if (item == null) return;

            // Only proceed if this is for one of the supported databases.
            if (!Config.SupportedDatabases.Any(d => d.Equals(item.Database.Name))) return;

            // Only proceed if something changed.
            if (!HasChanged(item)) return;

            // Make sure this is an ISavable entity before continuing.
            var savable = Helpers.EntityHelper.CreateSavableInstance(item);
            if (savable == null) return;

            // Now call the ISavable.Save method which will trigger custom save logic.
            var saved = savable.Save(item);
            if (saved) return;

            // If there was an error saving to external system, abort save process.
            ((SitecoreEventArgs) args).Result.Cancel = true;

            // Force UI to refresh to previous item values.
            var load =
                String.Concat(new object[]
                    {"item:load(id=", item.ID, ",language=", item.Language, ",version=", item.Version, ")"});
            Sitecore.Context.ClientPage.SendMessage(this, load);
        }

        private static bool HasChanged(Item current)
        {
            if (null == current) return false;

            var originalItem = current.Database.GetItem(current.ID, current.Language, current.Version);
            if (originalItem == null) return false;

            current.Fields.ReadAll();

            var fieldNames = current.Fields.Select(f => f.Name);
            var differences = fieldNames.Where(fieldName => current[fieldName] != originalItem[fieldName])
                                        .ToList();

            return differences.Any();
        }
    }
}