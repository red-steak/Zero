# Nastavení názvu projektu
$AppName = "DiaryApp"

Write-Host "=== 1. KROK: Zakládám Avalonia projekt přes dotnet CLI ===" -ForegroundColor Cyan

# Kontrola a instalace šablon (pokud chybí)
dotnet new install Avalonia.Templates --force | Out-Null

# Vytvoření projektu
if (Test-Path $AppName) {
    Write-Host "Složka $AppName již existuje, mazat ji nebudu, ale zkusím doplnit soubory." -ForegroundColor Yellow
} else {
    dotnet new avalonia.mvvm -o $AppName
}

# Přechod do složky projektu
Set-Location $AppName

Write-Host "=== 2. KROK: Instalace NuGet balíčků (CommunityToolkit.Mvvm, Markdig) ===" -ForegroundColor Cyan
dotnet add package CommunityToolkit.Mvvm
dotnet add package Markdig

Write-Host "=== 3. KROK: Vytváření adresářové struktury a zdrojových souborů ===" -ForegroundColor Cyan

# Vytvoření složek
$directories = @("Models", "ViewModels", "Views")
foreach ($dir in $directories) {
    if (!(Test-Path $dir)) {
        New-Item -ItemType Directory -Path $dir | Out-Null
        Write-Host "Vytvořena složka: $dir" -ForegroundColor Green
    }
}

# 1. Models/DiaryEntry.cs
$pathDiaryEntry = "Models\DiaryEntry.cs"
$contentDiaryEntry = @"
using System;

namespace DiaryApp.Models;

public class DiaryEntry
{
    public string FileName { get; set; } = string.Empty; // např. "2026-11-06.md"
    public DateTime Date { get; set; } = DateTime.Today;
    public string MarkdownContent { get; set; } = string.Empty;
    public string HtmlContent { get; set; } = string.Empty;
    public bool IsDirty { get; set; }
}
"@
Set-Content -Path $pathDiaryEntry -Value $contentDiaryEntry -Encoding utf8
Write-Host "Vytvořen soubor: $pathDiaryEntry" -ForegroundColor Yellow

# 2. ViewModels/DiaryEditorViewModel.cs
$pathEditorVm = "ViewModels\DiaryEditorViewModel.cs"
$contentEditorVm = @"
using System;
using System.Threading.Tasks;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Markdig;

namespace DiaryApp.ViewModels;

public partial class DiaryEditorViewModel : ObservableObject
{
    private readonly MarkdownPipeline _pipeline = new MarkdownPipelineBuilder()
        .UseAdvancedExtensions()
        .Build();

    [ObservableProperty]
    private string _markdownText = string.Empty;

    [ObservableProperty]
    private string _htmlPreview = string.Empty;

    [ObservableProperty]
    private DateTime _selectedDate = DateTime.Today;

    [ObservableProperty]
    private bool _isSaving;

    partial void OnMarkdownTextChanged(string value)
    {
        // Živý přepočet Markdown -> HTML
        HtmlPreview = Markdown.ToHtml(value ?? string.Empty, _pipeline);
    }

    [RelayCommand]
    private async Task SaveAndPublishAsync()
    {
        IsSaving = true;
        try
        {
            // 1. Zápis Markdownu a HTML souboru do lokálního adresáře
            // 2. Commit & Push v Gitu
            await Task.Delay(500); // Simulace I/O & Git operací
        }
        finally
        {
            IsSaving = false;
        }
    }
}
"@
Set-Content -Path $pathEditorVm -Value $contentEditorVm -Encoding utf8
Write-Host "Vytvořen soubor: $pathEditorVm" -ForegroundColor Yellow

# 3. ViewModels/DiaryListViewModel.cs
$pathListVm = "ViewModels\DiaryListViewModel.cs"
$contentListVm = @"
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
            FileName = `${DateTime.Today:yyyy-MM-dd}.md`,
            MarkdownContent = "# Nový zápis`n`nDneska..."
        };
        Entries.Insert(0, newEntry);
        SelectedEntry = newEntry;
    }
}
"@
Set-Content -Path $pathListVm -Value $contentListVm -Encoding utf8
Write-Host "Vytvořen soubor: $pathListVm" -ForegroundColor Yellow

# 4. ViewModels/MainViewModel.cs
$pathMainVm = "ViewModels\MainViewModel.cs"
$contentMainVm = @"
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
"@
Set-Content -Path $pathMainVm -Value $contentMainVm -Encoding utf8
Write-Host "Vytvořen soubor: $pathMainVm" -ForegroundColor Yellow

# 5. Views/MainView.axaml
$pathMainView = "Views\MainView.axaml"
$contentMainView = @"
<UserControl xmlns="https://github.com/avaloniaui"
             xmlns:x="http://schemas.microsoft.com/winfx/2006/xaml"
             xmlns:vm="using:DiaryApp.ViewModels"
             x:Class="DiaryApp.Views.MainView"
             x:DataType="vm:MainViewModel">

    <Grid ColumnDefinitions="250, *, *">
        <!-- Sloupec 1: Seznam zápisů -->
        <DockPanel Grid.Column="0">
            <Button DockPanel.Dock="Bottom" 
                    Content="+ Nový zápis" 
                    Command="{Binding ListViewModel.CreateNewEntryCommand}" 
                    HorizontalAlignment="Stretch" Margin="10"/>
            <ListBox ItemsSource="{Binding ListViewModel.Entries}" 
                     SelectedItem="{Binding ListViewModel.SelectedEntry}">
                <ListBox.ItemTemplate>
                    <DataTemplate>
                        <TextBlock Text="{Binding Date, StringFormat={}{0:yyyy-MM-dd}}" />
                    </DataTemplate>
                </ListBox.ItemTemplate>
            </ListBox>
        </DockPanel>

        <!-- Sloupec 2: Markdown Editor -->
        <DockPanel Grid.Column="1" Margin="10">
            <Button DockPanel.Dock="Bottom" 
                    Content="Uložit &amp; Publikovat (Commit &amp; Push)" 
                    Command="{Binding EditorViewModel.SaveAndPublishCommand}"
                    HorizontalAlignment="Stretch"/>
            <TextBox Text="{Binding EditorViewModel.MarkdownText, Mode=TwoWay}"
                     AcceptsReturn="True" 
                     TextWrapping="Wrap" 
                     Watermark="Napiš svůj dnešní zápis v Markdownu..." />
        </DockPanel>

        <!-- Sloupec 3: HTML Náhled -->
        <Border Grid.Column="2" Margin="10" Background="#1E1E1E" CornerRadius="5" Padding="10">
            <ScrollViewer>
                <SelectableTextBlock Text="{Binding EditorViewModel.HtmlPreview}" 
                                     TextWrapping="Wrap" />
            </ScrollViewer>
        </Border>
    </Grid>
</UserControl>
"@
Set-Content -Path $pathMainView -Value $contentMainView -Encoding utf8
Write-Host "Vytvořen soubor: $pathMainView" -ForegroundColor Yellow

Write-Host "`nHotovo! Projekt '$AppName' je kompletně vygenerovaný, závislosti jsou stažené a struktura je připravená." -ForegroundColor Green
Write-Host "Pro spuštění aplikace stačí napsat:" -ForegroundColor Cyan
Write-Host "cd $AppName" -ForegroundColor White
Write-Host "dotnet run" -ForegroundColor White