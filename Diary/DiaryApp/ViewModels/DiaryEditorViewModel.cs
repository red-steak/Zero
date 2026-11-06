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
        // Markdown -> HTML
        HtmlPreview = Markdig.Markdown.ToHtml(value ?? string.Empty, _pipeline);
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
