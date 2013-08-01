namespace SitecoreSuperchargers.GenericItemProvider.Attributes
{
    public interface IConvertibleAttribute
    {
        string Convert(string raw);
        string Unconvert(string raw);
    }
}