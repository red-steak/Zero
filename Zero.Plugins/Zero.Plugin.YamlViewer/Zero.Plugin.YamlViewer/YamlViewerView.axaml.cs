using Avalonia.Controls;

namespace Zero.Plugin.YamlViewer;

public partial class YamlViewerView : UserControl
{
    public YamlViewerView(YamlViewerViewModel vm)
    {
        InitializeComponent();
        DataContext = vm;
    }
}
