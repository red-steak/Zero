using Avalonia.Controls;
using System.Linq;
using Zero.App.ViewModels;

namespace Zero.App.Views;

public partial class MainWindow : Window
{
    public MainWindow(MainViewModel vm)
    {
        InitializeComponent();
        DataContext = vm;

        //vm.SelectedWorkspace = vm.Workspaces.FirstOrDefault();
    }
}