using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using DiaryApp.Enums;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace DiaryApp.ViewModels;

public partial class DiaryEntryViewModel: ViewModelBase
{
    public DiaryEntryViewModel(DiaryListViewModel diaryListViewModel)
    {
        DiaryListViewModel = diaryListViewModel;
        AllTags.CollectionChanged += (o, e) =>
        {
            foreach (var entry in DiaryListViewModel.Entries)
            {
                entry.AllTags = AllTags;
            }
        };
    }

    public DiaryListViewModel DiaryListViewModel { get; }

    [ObservableProperty] string _fileName = Guid.NewGuid().ToString(); 
    [ObservableProperty] DateTime _date  = DateTime.Now;
    [ObservableProperty] DateTimeOffset? _dateOffset;
    [ObservableProperty] string _markdownContent = string.Empty;
    [ObservableProperty] string _note = string.Empty;
    [ObservableProperty] WorkKind _workDayKind = WorkKind.Undefined;
    [ObservableProperty] ObservableCollection<Tag> _tags = [];
    [ObservableProperty] bool? _trip = false;
    [ObservableProperty] bool _freeTimeActivity = false;
    [ObservableProperty]
    private string _newTagInput = string.Empty;

    public ObservableCollection<Tag> AllTags { get; set; } = [];


    // Tato metoda se automaticky zavolá pokaždé, když se změní NewTagInput (díky CommunityToolkitu)
    partial void OnNewTagInputChanged(string value)
    {
        if (string.IsNullOrEmpty(value)) return;

        // Pokud uživatel napsal čárku, okamžitě oddělíme tag
        if (value.Contains(','))
        {
            var parts = value.Split(',', 2); // Rozdělíme na první část a zbytek
            var tagCandidate = parts[0].Trim();

            if (!string.IsNullOrEmpty(tagCandidate))
            {
                AddSingleTag(tagCandidate);
            }

            // Zbytek textu (pokud uživatel napsal víc naráz) vrátíme zpět do inputu
            NewTagInput = parts.Length > 1 ? parts[1].TrimStart() : string.Empty;
        }
    }

    [RelayCommand]
    private void AddTag()
    {
        if (string.IsNullOrWhiteSpace(NewTagInput)) return;

        // Podpora pro vložení více tagů oddělených čárkou (např. paste)
        var rawTags = NewTagInput.Split(',', StringSplitOptions.RemoveEmptyEntries);
        foreach (var rawTag in rawTags)
        {
            AddSingleTag(rawTag);
        }

        NewTagInput = string.Empty;
    }

    private void AddSingleTag(string rawTag)
    {
        var trimmedTag = rawTag.Trim().ToLowerInvariant();
        if (string.IsNullOrEmpty(trimmedTag)) return;

        // Přidání do aktuálního zápisu
        if (!Tags.Any(x => x.Value == trimmedTag))
        {
            Tags.Add(new Tag(trimmedTag, this));
        }

        // Přidání do globálního našeptávače
        if (!AllTags.Any(x => x.Value == trimmedTag))
        {
            AllTags.Add(new Tag(trimmedTag, this));
        }
    }

    [RelayCommand]
    private void RemoveTag(Tag tag)
    {
        if (Tags.Any(x => x.Value == tag.Value))
        {
            Tags.Remove(tag);
        }
        if (AllTags.Count(x => x.Value == tag.Value) == 1)
        {
            var allTagsTag = AllTags.First(x => x.Value == tag.Value);
            AllTags.Remove(allTagsTag);
        }
    }

    public List<WorkKindItem> WorkKinds { get; } =
    [
        new WorkKindItem("Nedefinováno", WorkKind.Undefined),
        new WorkKindItem("Bez práce", WorkKind.NoWork),
        new WorkKindItem("Ranní směna", WorkKind.MorningShift),
        new WorkKindItem("Odpolední směna", WorkKind.AfternoonShift),
        new WorkKindItem("Noční směna", WorkKind.NightShift),
        new WorkKindItem("Celý den", WorkKind.FullDay),
        new WorkKindItem("Ranní a večerní směna", WorkKind.MorningAndEveningShift)
    ];

    partial void OnMarkdownContentChanged(string value)
    {
        // Aktualizovat HTML náhled, když se změní Markdown obsah
        HtmlContent = Markdig.Markdown.ToHtml(value ?? string.Empty);
        EditorViewModel.MarkdownText = value ?? string.Empty;
    }

    partial void OnDateChanged(DateTime value)
    {
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
