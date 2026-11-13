using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System;
using System.Globalization;

namespace DiaryApp.ViewModels;

public partial class DiaryEntryViewModel: ViewModelBase
{
    [ObservableProperty] string _fileName = string.Empty; 
    [ObservableProperty] DateTime _date  = DateTime.Now;
    [ObservableProperty] DateTimeOffset? _dateOffset;
    [ObservableProperty] string _markdownContent = string.Empty;
    [ObservableProperty] string _note = string.Empty;

    partial void OnMarkdownContentChanged(string value)
    {
        // Aktualizovat HTML náhled, když se změní Markdown obsah
        HtmlContent = Markdig.Markdown.ToHtml(value ?? string.Empty);
        EditorViewModel.MarkdownText = value ?? string.Empty;
    }

    partial void OnDateChanged(DateTime value)
    {
        // Aktualizovat název souboru, když se změní datum
        FileName = $"{value:s}.md";
        _dateChanging = true;
        try
        {
            DateOffset = new DateTimeOffset(value.Date);
        }
        finally
        {
            _dateChanging = false;
        }
    }

    bool _dateChanging = false;

    partial void OnDateOffsetChanged(DateTimeOffset? value)
    {
        if (!_dateChanging && value.HasValue)
        {
            Date = Convert.ToDateTime(value.Value.Date);
        }
    }

    partial void OnHtmlContentChanged(string value)
    {
        // Můžete zde přidat logiku, pokud chcete reagovat na změny HTML obsahu
        EditorViewModel.HtmlPreview = value;
    }

    [ObservableProperty] string _htmlContent = string.Empty;
    [ObservableProperty] bool _isDirty = false;

    [ObservableProperty] DiaryEditorViewModel _editorViewModel = new();


}
