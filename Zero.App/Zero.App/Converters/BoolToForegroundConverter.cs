using Avalonia.Data.Converters;
using Avalonia.Media;
using System;
using System.Globalization;

namespace Zero.App.Converters
{
    public class BoolToForegroundConverter : IValueConverter
    {
        public object? Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
        {
            bool? isSelected = (bool?)value;
            if (isSelected.HasValue && isSelected.Value)
            {
                return new SolidColorBrush(Colors.White);
            }
            else
            {
                return new SolidColorBrush(Color.FromArgb(255, 2, 10, 2));
            }
        }

        public object? ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
