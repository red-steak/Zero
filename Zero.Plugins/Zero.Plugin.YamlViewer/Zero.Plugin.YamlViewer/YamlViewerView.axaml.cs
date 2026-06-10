using Avalonia.Controls;
using YamlDotNet.RepresentationModel;

namespace Zero.Plugin.YamlViewer;

public partial class YamlViewerView : UserControl
{
    public YamlViewerView(YamlViewerViewModel vm)
    {
        InitializeComponent();
        DataContext = vm;
    }
}
