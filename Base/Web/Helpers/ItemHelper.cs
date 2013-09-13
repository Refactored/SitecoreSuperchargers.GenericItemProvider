using Sitecore;
using Sitecore.Data.Fields;
using Sitecore.Data.Items;
using System.Collections.Generic;
using System.Linq;
using SitecoreSuperchargers.GenericItemProvider.Data;

namespace SitecoreSuperchargers.GenericItemProvider.Helpers
{
    public class ItemHelper
    {
        public static IEnumerable<Field> GetFieldChanges(Item item)
        {
            if (null == item) return new List<Field>();

            var existing = item.Database.GetItem(item.ID, item.Language, item.Version);
            return existing == null
                       ? new List<Field>()
                       : GetFieldChanges(item, existing);
        }

        public static IEnumerable<Field> GetFieldChanges(BaseItem item, BaseItem existing)
        {
            if (null == item) return new List<Field>();
            if (null == existing) return new List<Field>();

            item.Fields.ReadAll();

            var fields = item.Fields.ToList();
            var differences = fields.Where(f => f.Name != existing[f.Name]).ToList();
            return differences.Any() ? differences : new List<Field>();
        }

        public static bool HasChanged(Item item)
        {
            return GetFieldChanges(item).Any();
        }

        public static bool IsIgnorable(Item item)
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
            return !HasChanged(item);
        }
    }
}