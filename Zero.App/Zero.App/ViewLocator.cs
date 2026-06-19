using Avalonia;
using Avalonia.Controls;
using Avalonia.Controls.Templates;
using System;
using System.Diagnostics.CodeAnalysis;

namespace Zero.App;

/// <summary>
/// Given a view model, returns the corresponding view if possible.
/// </summary>
[RequiresUnreferencedCode(
    "Default implementation of ViewLocator involves reflection which may be trimmed away.",
    Url = "https://docs.avaloniaui.net/docs/concepts/view-locator")]
public class ViewLocator : IDataTemplate
{
    public Control? Build(object? param)
    {
        if (param is null)
            return null;

        var name = param.GetType().FullName!.Replace("ViewModel", "View", StringComparison.Ordinal);
        var type = Type.GetType(name);

        if (type != null)
        {
            try
            {
                var r = (Control)Activator.CreateInstance(type)!;
                return r;
            }
            catch (Exception ex)
            {
                return new TextBlock { Text = param.ToString(), Padding = new Thickness(1), Margin = new Thickness(0) };
            }
        }
        else
        {
            return new TextBlock { Text = param.ToString(), Padding = new Thickness(1), Margin = new Thickness(0) };
        }

    }

    public bool Match(object? data)
    {
        return true;
    }
}