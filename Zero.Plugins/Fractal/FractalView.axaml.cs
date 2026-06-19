using Avalonia.Controls;

namespace Zero.Plugin.Fractal
{
    public partial class FractalView : UserControl
    {
        public FractalView(FractalViewModel vm)
        {
            InitializeComponent();
            DataContext = vm;
        }
    }
}
