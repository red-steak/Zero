using Avalonia.Data.Converters;
using System;
using System.Globalization;
using Zero.Core.Ribbon;

namespace Zero.App.Converters;

public class RibbonSizeToHeightConverter : IValueConverter
{
    public object Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
        => value is RibbonSize size ? size switch
        {
            RibbonSize.Large => 60,
            RibbonSize.Medium => 30,
            RibbonSize.Small => 16,
            _ => 30
        } : 30;

    public object ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
        => throw new NotImplementedException();
}
