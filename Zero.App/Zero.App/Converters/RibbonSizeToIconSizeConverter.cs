using Avalonia.Data.Converters;
using Zero.Core.Ribbon;
using System;
using System.Globalization;

namespace Zero.App.Converters;

public class RibbonSizeToIconSizeConverter : IValueConverter
{
    public object Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
        => value is RibbonSize size ? size switch
        {
            RibbonSize.Large => 32,
            RibbonSize.Medium => 18,
            RibbonSize.Small => 10,
            _ => 18
        } : 18;

    public object ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
        => throw new NotImplementedException();
}
