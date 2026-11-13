using Avalonia;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Linq;
using System.Threading;

namespace DiaryApp.ViewModels;

public partial class DiaryListViewModel : ObservableObject
{
    [ObservableProperty]
    private ObservableCollection<DiaryEntryViewModel> _entries = new();

    [ObservableProperty]
    private DiaryEntryViewModel? _selectedEntry;

    public event Action<DiaryEntryViewModel>? EntrySelected;

    partial void OnSelectedEntryChanged(DiaryEntryViewModel? value)
    {
        if (value != null)
        {
            EntrySelected?.Invoke(value);
        }
    }

    [RelayCommand]
    void Sort()
    {
        Entries = [..Entries.OrderByDescending(x => x.Date)];
    }

    [RelayCommand]
    private void CreateNewEntry()
    {
        var date = DateTime.Now;
        var czechCulture = CultureInfo.GetCultureInfo("cs-CZ");
        var newEntry = new DiaryEntryViewModel
        {
            Date = date,
            FileName = $"{date:yyyy-MM-dd hh_mm_ss}.md",
            MarkdownContent = $"# Dnes je {date.ToString("rok yyyy d. MMMM [dddd]", czechCulture)}"
        };
        Entries.Insert(0, newEntry);
        SelectedEntry = newEntry;
    }
}
