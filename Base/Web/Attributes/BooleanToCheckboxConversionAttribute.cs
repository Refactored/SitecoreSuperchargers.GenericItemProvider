using System;

namespace SitecoreSuperchargers.GenericItemProvider.Attributes
{
    /// <summary>
    /// This attribute is used to convert a boolean (or bit) field from an external data store
    /// into a value that the Sitecore CheckboxField can interpret ("1" for true, empty string for false).
    /// </summary>
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false, Inherited = true)]
    public class BooleanToCheckboxConversionAttribute : ConvertibleFieldMapping
    {
        public BooleanToCheckboxConversionAttribute(string fieldId)
            : base(fieldId)
        {
        }

        public BooleanToCheckboxConversionAttribute(string fieldId, bool forTransation, bool disableSecurity = false)
            : base(fieldId, forTransation, disableSecurity)
        {
        }

        public override string Convert(string raw)
        {
            return raw.Trim().ToLower() == "true" ? "1" : "";
        }

        public override string Unconvert(string raw)
        {
            return raw.Trim().ToLower() == "1" ? "true" : "false";
        }
    }
}