using Sitecore.Data.Items;

namespace SitecoreSuperchargers.GenericItemProvider.Data
{
    public interface ICreatable : IEntity
    {
        bool Create(Item item);
    }
}