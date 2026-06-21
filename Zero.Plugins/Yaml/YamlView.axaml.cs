using Avalonia.Controls;

namespace Zero.Plugin.Yaml;

public partial class YamlView : UserControl
{
    public YamlView(YamlViewModel vm)
    {
        InitializeComponent();
        DataContext = vm;
    }
}
