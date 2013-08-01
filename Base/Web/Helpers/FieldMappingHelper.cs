using Sitecore.StringExtensions;
using SitecoreSuperchargers.GenericItemProvider.Attributes;
using SitecoreSuperchargers.GenericItemProvider.Data;
using System;
using System.Reflection;

namespace SitecoreSuperchargers.GenericItemProvider.Helpers
{
    public class FieldMappingHelper
    {
        public static string GetFieldId(PropertyInfo property)
        {
            var mapping = GetFieldMapping(property);
            if (mapping == null) return String.Empty;

            return mapping.FieldId.IsNullOrEmpty() ? String.Empty : mapping.FieldId;
        }

        public static FieldMapping GetFieldMapping(PropertyInfo property)
        {
            var attributes = property.GetCustomAttributes(typeof(FieldMapping), true);
            return attributes.Length <= 0 ? null : attributes[0] as FieldMapping;
        }

        public static string GetPropertyValue(PropertyInfo property, IEntity entity)
        {
            var mapping = GetFieldMapping(property);
            var rawValue = property.GetValue(entity, null);
            if (null != rawValue)
            {
                if (mapping is IConvertibleAttribute)
                {
                    var convertibleMapping = mapping as IConvertibleAttribute;
                    return convertibleMapping.Convert(rawValue.ToString());
                }

                return rawValue.ToString();
            }

            return String.Empty;
        }
    }
}