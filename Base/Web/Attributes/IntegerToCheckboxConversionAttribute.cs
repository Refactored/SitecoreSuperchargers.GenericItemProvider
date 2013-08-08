using System;

namespace SitecoreSuperchargers.GenericItemProvider.Attributes
{
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false, Inherited = true)]
    public class IntegerToCheckboxConversionAttribute : ConvertibleFieldMapping
    {
        public IntegerToCheckboxConversionAttribute(string fieldId)
            : base(fieldId)
        {
        }

        public IntegerToCheckboxConversionAttribute(string fieldId, bool forTransation, bool disableSecurity = false)
            : base(fieldId, forTransation, disableSecurity)
        {
        }

        public override string Convert(string raw)
        {
            return raw.Trim().ToLower() == "1" ? "1" : "";
        }

        public override string Unconvert(string raw)
        {
            // --- There really is no unconvert for this, it's either 0 or 1.
            return Convert(raw);
        }
    }
}