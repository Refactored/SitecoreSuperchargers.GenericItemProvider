using Sitecore.Data.Items;

namespace SitecoreSuperchargers.GenericItemProvider.Data
{
    public interface ISavable : IEntity
    {
        bool Save(Item item);
    }
}