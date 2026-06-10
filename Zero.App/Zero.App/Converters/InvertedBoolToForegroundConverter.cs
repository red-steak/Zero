using Avalonia.Data.Converters;
using Avalonia.Media;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;

namespace Zero.App.Converters
{
    public class InvertedBoolToForegroundConverter : IValueConverter
    {
        public object? Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
        {
            bool? isSelected = (bool?)value;
            if (isSelected.HasValue && isSelected.Value) 
            {
                return new SolidColorBrush(Color.FromArgb(128, 255, 255, 255));
            }
            else
            {
                return new SolidColorBrush(Colors.White);
            }
        }

        public object? ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
