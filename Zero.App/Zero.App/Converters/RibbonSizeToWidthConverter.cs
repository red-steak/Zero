using Avalonia.Data.Converters;
using System;
using System.Globalization;
using Zero.Core.Ribbon;

namespace Zero.App.Converters;

public class RibbonSizeToWidthConverter : IValueConverter
{
    public object Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
        => value is RibbonSize size ? size switch
        {
            RibbonSize.Large => 80,
            RibbonSize.Medium => 34,
            RibbonSize.Small => 26,
            _ => 34
        } : 34;

    public object ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
        => throw new NotImplementedException();
}
