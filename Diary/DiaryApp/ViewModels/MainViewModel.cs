using Avalonia.Collections;
using Avalonia.Platform.Storage;
using AvaloniaEdit.Utils;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using DiaryApp.Servicies;
using Markdown.Avalonia.Html.Core;
using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;

namespace DiaryApp.ViewModels;

public partial class MainViewModel : ViewModelBase
{
    public DiaryListViewModel ListViewModel { get; } = new();
    public ISettingsService Service { get; }
    public IFilesService FilesService { get; }

    public MainViewModel(ISettingsService settingsService, IFilesService filesService)
    {
        Service = settingsService;
        FilesService = filesService;

        Messages.CollectionChanged += (o, e) =>
        {
            if (Messages.Any())
            {
                LastMessage = Messages.Last();
            }
        };
    }

    [RelayCommand]
    private async Task OnLoadedAsync()
    {
        // Inicializační logika
        var settings = await Service.LoadSettingsAsync();
        Folder = settings.Folder ?? string.Empty;
        var messFolder = $"Nastavení načtena. Pracovní složka: {Folder}";
        MessSuccess(messFolder);

        var files = await FilesService.LoadFilesAsync();

        ListViewModel.Entries = [
            ..files.Select(f =>
            {
                var entry = new DiaryEntryViewModel(ListViewModel)
                {
                    FileName = f.FileName,
                    Note = f.Note,
                    MarkdownContent = f.MarkdownContent,
                    Date = f.DateAndTime,
                    IsDirty = false,
                    Trip = f.Trip,
                    WorkDayKind = f.WorkKind,
                };

                entry.Tags = [..f.Tags.Select(x => new Tag(x, entry))];

                return entry;
            })
        ];

        MessDownload();

        ListViewModel.EntrySelected += OnEntrySelected;

        if (ListViewModel.Entries.Any())
        {
            ListViewModel.SelectedEntry = ListViewModel.Entries.First();
        }
        else
        {
            ListViewModel.CreateNewEntryCommand.Execute(null);
            var mess = $"Vytvořen první záznam: {ListViewModel.SelectedEntry?.FileName}";
            MessExecuted(mess);
        }

        var allTags = new ObservableCollection<Tag>();
        foreach (var entry in ListViewModel.Entries)
        {
            foreach (var tag in entry.Tags)
            {
                if (!allTags.Any(x => x.Value == tag.Value))
                {
                    allTags.Add(tag);
                }
            }
        }
        ListViewModel.SelectedEntry!.Tags = allTags;
    }

    private void MessDownload()
    {
        Messages.Add(new MessageViewModel
        {
            Message = $"Načteno {ListViewModel.Entries.Count} souborů.",
            Timestamp = DateTime.Now,
            Kind = Enums.MessageKind.Download
        });
    }

    [ObservableProperty]
    string _folder = string.Empty;

    partial void OnFolderChanged(string value)
    {
        Folder = value;
    }

    public ObservableCollection<MessageViewModel> Messages { get; } = [];
    [ObservableProperty] MessageViewModel? _lastMessage;

    [RelayCommand]
    private async Task SelectFolderAsync(IStorageProvider? storageProvider)
    {
        if (storageProvider == null) return;

        var folders = await storageProvider.OpenFolderPickerAsync(new FolderPickerOpenOptions
        {
            Title = "Vyberte pracovní složku",
            AllowMultiple = false
        });

        if (folders.Any())
        {
            Folder = folders[0].TryGetLocalPath() ?? string.Empty;
            try
            {
                await Service.SaveSettingsAsync(new Models.Settings { Folder = Folder });
            }
            catch (Exception ex)
            {
                var messEx = $"Chyba při ukládání nastavení: {ex.Message}";
                MessError(messEx);
                return;
            }

            var mess = $"Pracovní složka nastavena na: {Folder}";
            MessExecuted(mess);
        }
    }

    private void MessExecuted(string mess)
    {
        Messages.Add(new MessageViewModel
        {
            Message = mess,
            Timestamp = DateTime.Now,
            Kind = Enums.MessageKind.Executed
        });
    }

    [RelayCommand]
    async Task SaveAndPublishAsync()
    {
        await SaveFilesAsync();
    }

    async Task SaveFilesAsync()
    {
        var (success, exception) = await FilesService.SaveFilesAsync([.. ListViewModel.Entries.Select(e => new Models.FileData
        {
            FileName = e.FileName,
            MarkdownContent = e.MarkdownContent,
            Note = e.Note,
            DateAndTime = e.Date,
            FreeTimeActivity = e.FreeTimeActivity,
            Tags = [.. e.Tags.Select(x => x.Value)],
            Trip = e.Trip.HasValue && e.Trip.Value,
            WorkKind = e.WorkDayKind
        })]);

        if (!success && exception != null)
        {
            // Zde můžete přidat logiku pro zobrazení chybové zprávy uživateli
            var mess = $"Chyba při ukládání souborů: {exception.Message}";
            Console.WriteLine(mess);
            MessError(mess);
        }
        else
        {
            MessUploaded();
        }

    }

    private void MessError(string mess)
    {
        Messages.Add(new MessageViewModel
        {
            Message = mess,
            Timestamp = DateTime.Now,
            Kind = Enums.MessageKind.Error
        });
    }

    private void MessSuccess(string mess)
    {
        Messages.Add(new MessageViewModel
        {
            Message = mess,
            Timestamp = DateTime.Now,
            Kind = Enums.MessageKind.Success
        });
    }

    private void MessUploaded()
    {
        Messages.Add(new MessageViewModel
        {
            Message = $"Počet: [{ListViewModel.Entries.Count}] Soubor(y) úspěšně uložen(y) a publikován(y).",
            Timestamp = DateTime.Now,
            Kind = Enums.MessageKind.Upload
        });
    }

    private void OnEntrySelected(DiaryEntryViewModel entry)
    {
        Title = $"Diary App - {entry.FileName}";
        MessSelected(entry);
    }

    private void MessSelected(DiaryEntryViewModel entry)
    {
        Messages.Add(new MessageViewModel
        {
            Message = $"Záznam vybrán: {entry.FileName}",
            Timestamp = DateTime.Now,
            Kind = Enums.MessageKind.Selected
        });
    }
}
