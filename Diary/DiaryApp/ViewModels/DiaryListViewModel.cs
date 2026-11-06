using System;
using System.Collections.ObjectModel;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using DiaryApp.Models;

namespace DiaryApp.ViewModels;

public partial class DiaryListViewModel : ObservableObject
{
    [ObservableProperty]
    private ObservableCollection<DiaryEntry> _entries = new();

    [ObservableProperty]
    private DiaryEntry? _selectedEntry;

    public event Action<DiaryEntry>? EntrySelected;

    partial void OnSelectedEntryChanged(DiaryEntry? value)
    {
        if (value != null)
        {
            EntrySelected?.Invoke(value);
        }
    }

    [RelayCommand]
    private void CreateNewEntry()
    {
        var newEntry = new DiaryEntry
        {
            Date = DateTime.Today,
            FileName = $"{DateTime.Today:yyyy-MM-dd}.md",
            MarkdownContent = "# Dnes ..."
        };
        Entries.Insert(0, newEntry);
        SelectedEntry = newEntry;
    }
}
