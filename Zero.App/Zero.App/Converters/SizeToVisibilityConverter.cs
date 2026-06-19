using Avalonia.Data.Converters;
using System;
using System.Globalization;
using Zero.Core.Ribbon;

namespace Zero.App.Converters
{
    public class SizeToVisibilityConverter : IValueConverter
    {
        public object? Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
        => value is RibbonSize size && size switch
        {
            RibbonSize.Large => true,
            RibbonSize.Medium => false,
            RibbonSize.Small => false,
            _ => false
        };

        public object? ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
