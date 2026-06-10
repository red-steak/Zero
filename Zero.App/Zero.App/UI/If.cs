using Avalonia;
using Avalonia.Controls;
using System;

namespace Zero.App.UI
{
    public class If : ContentControl
    {
        public static readonly StyledProperty<object?> ConditionProperty =
            AvaloniaProperty.Register<If, object?>(nameof(Condition));

        public static readonly StyledProperty<object?> EqualsProperty =
            AvaloniaProperty.Register<If, object?>(nameof(Equals));

        public static readonly StyledProperty<object?> ElseProperty =
            AvaloniaProperty.Register<If, object?>(nameof(Else));

        public object? Condition
        {
            get => GetValue(ConditionProperty);
            set => SetValue(ConditionProperty, value);
        }

        new public object? Equals
        {
            get => GetValue(EqualsProperty);
            set => SetValue(EqualsProperty, value);
        }

        public object? Else
        {
            get => GetValue(ElseProperty);
            set => SetValue(ElseProperty, value);
        }

        static If()
        {
            ConditionProperty.Changed.AddClassHandler<If>((x, _) => x.Update());
            EqualsProperty.Changed.AddClassHandler<If>((x, _) => x.Update());
            ElseProperty.Changed.AddClassHandler<If>((x, _) => x.Update());
        }

        private void Update()
        {
            if (Condition == null)
            {
                Content = Else;
                return;
            }

            // 🔥 ENUM POROVNÁNÍ
            if (Equals != null)
            {
                // Pokud Condition je enum a Equals je string → převedeme
                if (Condition.GetType().IsEnum && Equals is string s)
                {
                    var enumValue = Enum.Parse(Condition.GetType(), s);
                    Content = Condition.Equals(enumValue) ? base.Content : Else;
                    return;
                }
            }

            // fallback
            Content = Condition.Equals(Equals) ? base.Content : Else;
        }
    }
}