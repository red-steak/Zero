using CommunityToolkit.Mvvm.ComponentModel;
using DiaryApp.Enums;
using System;

namespace DiaryApp.ViewModels;

public partial class MessageViewModel : ViewModelBase
{
    [ObservableProperty] string _message = string.Empty;
    [ObservableProperty] DateTime _timestamp = DateTime.Now;
    [ObservableProperty] MessageKind _kind = MessageKind.Undefined;
}
