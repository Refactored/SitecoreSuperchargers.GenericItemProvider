using Sitecore.Data.Items;
using Sitecore.Reflection;
using SitecoreSuperchargers.GenericItemProvider.Data;
using System;
using System.Linq;

namespace SitecoreSuperchargers.GenericItemProvider.Helpers
{
    public class EntityHelper
    {
        /// <summary>
        /// Takes a Sitecore Item and determines which IEntity class to use. Through 
        /// reflection an IEntity instance is created and returned.
        /// </summary>
        /// <param name="item">The Sitecore Item that is represented by the IEntity</param>
        /// <returns>An instantiated but unpopulated IEntity object</returns>
        public static IEntity CreateEntityInstance(Item item)
        {
            //
            // Assumption here is that parent item is the container, it's possible that 
            // might not be the case so we should walk up the item tree until we find the
            // right node (this might be simpler if we defined an IRepositoryContainer
            // data template in Sitecore to use for this detection.
            //

            var typeString = item.Parent.Fields["Type"].GetValue(false);
            if (String.IsNullOrEmpty(typeString)) return null;

            var typeArray = typeString.Split(',').Select(p => p.Trim()).ToArray();
            if (!typeArray.Any()) return null;

            var assembly = ReflectionUtil.LoadAssembly(typeArray[1]);
            return Activator.CreateInstance(assembly.GetType(typeArray[0])) as IEntity;
        }

        /// <summary>
        /// Takes a Sitecore Item and determines which ISavable class to use. Through
        /// reflection an ISavable instance is created and returned.
        /// </summary>
        /// <param name="item">The Sitecore Item that is represented by the ISavable</param>
        /// <returns>An instantiated but unpopulated ISavable object</returns>
        public static ISavable CreateSavableInstance(Item item)
        {
            return CreateEntityInstance(item) as ISavable;
        }
    }
}