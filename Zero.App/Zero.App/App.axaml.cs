using Avalonia;
using Avalonia.Controls;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Markup.Xaml;
using Avalonia.Styling;
using Microsoft.Extensions.DependencyInjection;
using System;
using Zero.App.ViewModels;

namespace Zero.App;

public partial class App : Application
{
    private readonly IServiceProvider _provider;

    public App(IServiceProvider provider)
    {
        _provider = provider;
    }

    public override void Initialize()
    {
        AvaloniaXamlLoader.Load(this);
    }

    public override void OnFrameworkInitializationCompleted()
    {
        if (ApplicationLifetime is ISingleViewApplicationLifetime singleView)
        {
            singleView.MainView = (Control)_provider.GetRequiredService<Views.MainView>();
            singleView.MainView.DataContext = _provider.GetRequiredService<MainViewModel>();
        }

        if (ApplicationLifetime is IClassicDesktopStyleApplicationLifetime destopView)
        {
            destopView.MainWindow = (Window)_provider.GetRequiredService<Views.MainWindow>();
            destopView.MainWindow.DataContext = _provider.GetRequiredService<MainViewModel>();
        }

        RequestedThemeVariant = ThemeVariant.Dark;

        base.OnFrameworkInitializationCompleted();
    }
}