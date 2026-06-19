using Avalonia.Controls;

namespace Zero.Plugin.Canvas;

public partial class CanvasView : UserControl
{
    public CanvasView(CanvasViewModel vm)
    {
        InitializeComponent();
        DataContext = vm;
    }
}
