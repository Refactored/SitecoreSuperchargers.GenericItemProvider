using System.Collections.Generic;
using Sitecore.Data.Items;

namespace SitecoreSuperchargers.GenericItemProvider.Data
{
    public interface ISavable : IEntity
    {
        bool Save(Item savableItem, Item originalItem, IEnumerable<string> fieldChanges);
    }
}