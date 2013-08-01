using System;
using Sitecore;
using Sitecore.Exceptions;

namespace SitecoreSuperchargers.GenericItemProvider.Attributes
{
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false, Inherited = true)]
    public class DateTimeToDateConversionAttribute : ConvertibleFieldMapping
    {
        public DateTimeToDateConversionAttribute(string fieldId)
            : base(fieldId)
        {
        }

        public override string Convert(string raw)
        {
            DateTime date;

            if (!DateTime.TryParse(raw, out date))
            {
                throw new InvalidTypeException("DateTime is expected as a parameter");
            }

            // Reformat date to drop seconds as the date/time picker in Sitecore UI does not pass  
            // along the seconds which causes issues with read/write support (new version every 
            // time because date values will not match external data store).
            DateTime.TryParse(date.ToString("g"), out date);
            
            return DateUtil.ToIsoDate(date);
        }

        public override string Unconvert(string raw)
        {
            return String.IsNullOrEmpty(raw) ? String.Empty : DateUtil.FormatIsoDate(raw);
        }
    }
}