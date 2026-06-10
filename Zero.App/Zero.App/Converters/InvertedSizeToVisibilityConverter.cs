using Avalonia.Data.Converters;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using System.Transactions;
using Zero.Core.Ribbon;

namespace Zero.App.Converters
{
    public class InvertedSizeToVisibilityConverter : IValueConverter
    {
        public object? Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
        => value is RibbonSize size && size switch
        {
            RibbonSize.Large => false,
            RibbonSize.Medium => true,
            RibbonSize.Small => true,
            _ => true
        };

        public object? ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
