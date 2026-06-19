using Avalonia;
using Avalonia.Animation;
using Avalonia.Controls;
using Avalonia.Media;
using Avalonia.Styling;
using System;
using System.Threading.Tasks;
using Zero.Core.Workspaces;

namespace Zero.App.UI
{
    public class RotatingImage : Image
    {
        public static readonly StyledProperty<MessageKind> KindProperty =
            AvaloniaProperty.Register<RotatingImage, MessageKind>(nameof(Kind));

        static RotatingImage()
        {
            KindProperty.Changed.AddClassHandler<RotatingImage>((x, _) => { x.Update(); x._lastKind = x.Kind; });
        }

        private MessageKind _lastKind = MessageKind.Unknown;

        public MessageKind Kind
        {
            get => GetValue(KindProperty);
            set => SetValue(KindProperty, value);
        }

        public override void EndInit()
        {

            base.EndInit();
            // 1. Nastavíme střed rotace přesně na prostředek obrázku (50 % šířky, 50 % výšky)
            RenderTransformOrigin = new RelativePoint(0.5, 0.5, RelativeUnit.Relative);

            Update();
        }

        void Update()
        {
            if (Kind == MessageKind.Success || Kind == MessageKind.Saving || Kind == MessageKind.Warning || Kind == MessageKind.Error || Kind == MessageKind.Working) Dispatcher.InvokeAsync(Blick);
            if (Kind == MessageKind.Loading) Dispatcher.InvokeAsync(Rotate);
        }


        async Task Blick()
        {
            // 2. Přiřadíme transformaci, kterou budeme animovat
            RenderTransform = new ScaleTransform();

            // 3. Nadeklarujeme samotnou animaci
            var animation = new Animation
            {
                Duration = TimeSpan.FromSeconds(1.5),       // Jak dlouho trvá jedna otočka o 360 stupňů
                IterationCount = IterationCount.Parse("2"),
                Children =
            {
                new KeyFrame
                {
                    KeyTime = TimeSpan.Zero,
                    Setters = { new Setter(ScaleTransform.ScaleXProperty, 0.5), new Setter(ScaleTransform.ScaleYProperty, 0.5) }
                },
                new KeyFrame
                {
                    KeyTime = TimeSpan.FromSeconds(1.5), // Čas by měl odpovídat Duration
                    Setters = { new Setter(ScaleTransform.ScaleXProperty, 1), new Setter(ScaleTransform.ScaleYProperty, 1) }
                }
            }
            };

            // 4. Odpálíme to asynchronně na aktuálním prvku (this)
            await animation.RunAsync(this);
        }


        async Task Rotate()
        {
            // 1. Nastavíme střed rotace přesně na prostředek obrázku (50 % šířky, 50 % výšky)
            RenderTransformOrigin = new RelativePoint(0.5, 0.5, RelativeUnit.Relative);

            // 2. Přiřadíme transformaci, kterou budeme animovat
            RenderTransform = new RotateTransform();

            // 3. Nadeklarujeme samotnou animaci
            var animation = new Animation
            {
                Duration = TimeSpan.FromSeconds(1.5),       // Jak dlouho trvá jedna otočka o 360 stupňů
                IterationCount = IterationCount.Infinite,
                Children =
            {
                new KeyFrame
                {
                    KeyTime = TimeSpan.Zero,
                    Setters = { new Setter(RotateTransform.AngleProperty, 0d) }
                },
                new KeyFrame
                {
                    KeyTime = TimeSpan.FromSeconds(1.5), // Čas by měl odpovídat Duration
                    Setters = { new Setter(RotateTransform.AngleProperty, 360d) }
                }
            }
            };

            // 4. Odpálíme to asynchronně na aktuálním prvku (this)
            await animation.RunAsync(this);
        }
    }
}
