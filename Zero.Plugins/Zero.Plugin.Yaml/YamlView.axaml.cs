using Avalonia.Controls;
using YamlDotNet.RepresentationModel;

namespace Zero.Plugin.Yaml;

public partial class YamlView : UserControl
{
    public YamlView(YamlViewModel vm)
    {
        InitializeComponent();
        DataContext = vm;
    }
}
