using System;
using System.Globalization;
using Avalonia.Data.Converters;
using DiaryApp.Enums;

namespace DiaryApp.Converters
{
    internal class MessageKindWarningToVisibilityConverter : IValueConverter
    {
        public object Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
        {
            MessageKind messageKind = value is MessageKind kind ? kind : MessageKind.Undefined;

            bool isVisible = KindFactory.IsWarning(messageKind);

            return isVisible;
        }

        public object ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
        {
            return new Avalonia.Data.BindingNotification(value);
        }
    }
}
