using Sitecore.Data.Fields;
using Sitecore.Data.Items;
using System.Collections.Generic;
using System.Linq;

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
    }
}