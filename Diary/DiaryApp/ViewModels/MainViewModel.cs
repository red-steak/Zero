using CommunityToolkit.Mvvm.ComponentModel;
using DiaryApp.Models;

namespace DiaryApp.ViewModels;

public partial class MainViewModel : ObservableObject
{
    public DiaryListViewModel ListViewModel { get; } = new();
    public DiaryEditorViewModel EditorViewModel { get; } = new();

    public MainViewModel()
    {
        ListViewModel.EntrySelected += OnEntrySelected;
    }

    private void OnEntrySelected(DiaryEntry entry)
    {
        EditorViewModel.SelectedDate = entry.Date;
        EditorViewModel.MarkdownText = entry.MarkdownContent;
    }
}
