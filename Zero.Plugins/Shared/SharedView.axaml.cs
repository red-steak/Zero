using Avalonia.Controls;

namespace Zero.Plugin.Shared
{
    public partial class SharedView : UserControl
    {
        public SharedView(SharedViewModel vm)
        {
            InitializeComponent();
            DataContext = vm;
        }
    }
}
