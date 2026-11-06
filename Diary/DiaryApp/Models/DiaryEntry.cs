using System;

namespace DiaryApp.Models;

public class DiaryEntry
{
    public string FileName { get; set; } = string.Empty; // napĹ™. "2026-11-06.md"
    public DateTime Date { get; set; } = DateTime.Today;
    public string MarkdownContent { get; set; } = string.Empty;
    public string HtmlContent { get; set; } = string.Empty;
    public bool IsDirty { get; set; }
}
