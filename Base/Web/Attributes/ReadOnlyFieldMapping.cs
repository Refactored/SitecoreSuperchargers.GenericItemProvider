
namespace SitecoreSuperchargers.GenericItemProvider.Attributes
{
    /// <summary>
    /// This attribute is used by savable entities to flag a field that is read-only and therefore not updatable.
    /// </summary>
    public class ReadOnlyFieldMapping : FieldMapping
    {
        public ReadOnlyFieldMapping(string fieldId) : base(fieldId)
        {
        }

        public ReadOnlyFieldMapping(string fieldId, bool forTransation, bool disableSecurity = false)
            : base(fieldId, forTransation, disableSecurity)
        {
        }
    }
}