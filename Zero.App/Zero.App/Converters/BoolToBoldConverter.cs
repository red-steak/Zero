using Avalonia.Data.Converters;
using Avalonia.Media;
using System;
using System.Globalization;

namespace Zero.App.Converters
{
    internal class BoolToBoldConverter : IValueConverter
    {
        public object? Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
        {
            bool? b = System.Convert.ToBoolean(value);
            if (b is null || !b.Value)
            {
                return FontWeight.Light;
            }
            else 
            {
                return FontWeight.Bold;
            }
        }

        public object? ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
