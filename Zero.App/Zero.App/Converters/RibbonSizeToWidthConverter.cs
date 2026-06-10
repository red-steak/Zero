using Avalonia.Data.Converters;
using Zero.Core.Ribbon;
using System;
using System.Globalization;

namespace Zero.App.Converters;

public class RibbonSizeToWidthConverter : IValueConverter
{
    public object Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
        => value is RibbonSize size ? size switch
        {
            RibbonSize.Large => 128,
            RibbonSize.Medium => 34,
            RibbonSize.Small => 26,
            _ => 40
        } : 40;

    public object ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
        => throw new NotImplementedException();
}
