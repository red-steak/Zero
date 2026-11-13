using CommunityToolkit.Mvvm.ComponentModel;

namespace DiaryApp.ViewModels;

public partial class ViewModelBase : ObservableObject
{
    [ObservableProperty]
    string _title = "Diary App";
}
