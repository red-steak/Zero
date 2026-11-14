using Avalonia.Collections;
using Avalonia.Platform.Storage;
using AvaloniaEdit.Utils;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using DiaryApp.Servicies;
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
    }

    [RelayCommand]
    private async Task OnLoadedAsync()
    {
        Messages.CollectionChanged += (o, e) =>
        {
            if (Messages.Any())
            {
                LastMessage = Messages.Last();
            }
        };

        // Inicializační logika
        var settings = await Service.LoadSettingsAsync();
        Folder = settings.Folder ?? string.Empty;

        Messages.Add(new MessageViewModel
        {
            Message = $"Nastavení načtena. Pracovní složka: {Folder}",
            Timestamp = DateTime.Now,
            Kind = Enums.MessageKind.Success
        });

        var files = await FilesService.LoadFilesAsync();

        ListViewModel.Entries = [.. files.Select(f => new DiaryEntryViewModel()
        {
            FileName = f.FileName,
            Note = f.Note,
            MarkdownContent = f.Content
        })];

        Messages.Add(new MessageViewModel
        {
            Message = $"Načteno {ListViewModel.Entries.Count} souborů.",
            Timestamp = DateTime.Now,
            Kind = Enums.MessageKind.Download
        });

        ListViewModel.EntrySelected += OnEntrySelected;

        if (ListViewModel.Entries.Any())
        {
            ListViewModel.SelectedEntry = ListViewModel.Entries.First();
        }
        else
        {
            ListViewModel.CreateNewEntryCommand.Execute(null);
            Messages.Add(new MessageViewModel
            {
                Message = $"Vytvořen první záznam: {ListViewModel.SelectedEntry?.FileName}",
                Timestamp = DateTime.Now,
                Kind = Enums.MessageKind.Executed
            });
        }

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
                Messages.Add(new MessageViewModel
                {
                    Message = $"Chyba při ukládání nastavení: {ex.Message}",
                    Timestamp = DateTime.Now,
                    Kind = Enums.MessageKind.Error
                });
                return;
            }

            Messages.Add(new MessageViewModel
            {
                Message = $"Pracovní složka nastavena na: {Folder}",
                Timestamp = DateTime.Now,
                Kind = Enums.MessageKind.Info
            });
        }
    }

    [RelayCommand]
    async Task SaveAndPublishAsync()
    {
        await SaveFilesAsync();
    }

    async Task SaveFilesAsync()
    {
        var (success, exception) = await FilesService.SaveFilesAsync([.. ListViewModel.Entries.Select(e => new Models.MdFile
        {
            FileName = e.FileName,
            Content = e.MarkdownContent,
            Note = e.Note
        })]);

        if (!success && exception != null)
        {
            // Zde můžete přidat logiku pro zobrazení chybové zprávy uživateli
            Console.WriteLine($"Chyba při ukládání souborů: {exception.Message}");
            Messages.Add(new MessageViewModel
            {
                Message = $"Chyba při ukládání souborů: {exception.Message}",
                Timestamp = DateTime.Now,
                Kind = Enums.MessageKind.Error
            });
        }
        else
        {
            Messages.Add(new MessageViewModel
            {
                Message = $"Počet: [{ListViewModel.Entries.Count}] Soubor(y) úspěšně uložen(y) a publikován(y).",
                Timestamp = DateTime.Now,
                Kind = Enums.MessageKind.Upload
            });
        }

    }

    private void OnEntrySelected(DiaryEntryViewModel entry)
    {
        Title = $"Diary App - {entry.FileName}";
        Messages.Add(new MessageViewModel
        {
            Message = $"Záznam vybrán: {entry.FileName}",
            Timestamp = DateTime.Now,
            Kind = Enums.MessageKind.Selected
        });
    }
}
