using Sitecore.Data;
using Sitecore.Data.Fields;
using Sitecore.Data.Items;
using SitecoreSuperchargers.GenericItemProvider.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;

namespace SitecoreSuperchargers.GenericItemProvider.Data
{
    public abstract class AbstractSavable : ISavable
    {
        public abstract string GetItemName();
        public abstract string GetLanguageName();
        public abstract bool Create(IEntity savableEntity);
        public abstract bool Save(Item savableItem, Item originalItem, IEnumerable<Field> fieldChanges);

        public void ResetReadOnlyProperties(Item originalItem, IEnumerable<Field> fieldChanges)
        {
            var readOnlyProperties =
                this.GetType()
                    .GetProperties()
                    .Where(p => p.GetCustomAttributes(typeof (ReadOnlyFieldMapping), true).Length > 0)
                    .ToArray();
            foreach (var property in readOnlyProperties)
            {
                var fieldMapping =
                    property.GetCustomAttributes(typeof (ReadOnlyFieldMapping), true).FirstOrDefault() as
                    ReadOnlyFieldMapping;
                if (null == fieldMapping) continue;

                property.SetValue(
                    this,
                    Convert.ChangeType(
                        originalItem.Fields[new ID(fieldMapping.FieldId)].GetValue(true),
                        property.PropertyType
                        ),
                    null
                    );
            }
        }
    }
}