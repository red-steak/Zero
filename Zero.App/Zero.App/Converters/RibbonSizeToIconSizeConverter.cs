using Avalonia.Data.Converters;
using System;
using System.Globalization;
using Zero.Core.Ribbon;

namespace Zero.App.Converters;

public class RibbonSizeToIconSizeConverter : IValueConverter
{
    public object Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
        => value is RibbonSize size ? size switch
        {
            RibbonSize.Large => 32,
            RibbonSize.Medium => 24,
            RibbonSize.Small => 20,
            _ => 24
        } : 24;

    public object ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
        => throw new NotImplementedException();
}
