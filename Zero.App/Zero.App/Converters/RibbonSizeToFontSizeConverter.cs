using Avalonia.Data.Converters;
using Zero.Core.Ribbon;
using System;
using System.Globalization;

namespace Zero.App.Converters;

public class RibbonSizeToFontSizeConverter : IValueConverter
{
    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        => value is RibbonSize size ? size switch
        {
            RibbonSize.Large => 12,
            RibbonSize.Medium => 10,
            RibbonSize.Small => 8,
            _ => 10
            
        } : 10;

    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        => throw new NotImplementedException();
}
