using System.Collections.Generic;
using Sitecore.Data.Fields;
using Sitecore.Data.Items;

namespace SitecoreSuperchargers.GenericItemProvider.Data
{
    public interface ISavable : IEntity
    {
        bool Save(Item savableItem, Item originalItem, IEnumerable<Field> fieldChanges);
    }
}