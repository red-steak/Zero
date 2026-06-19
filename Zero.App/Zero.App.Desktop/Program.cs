using Avalonia;
using Microsoft.Extensions.DependencyInjection;
using ReactiveUI.Avalonia;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Zero.App.ViewModels;
using Zero.App.Views;
using Zero.Core.Pipelines;
using Zero.Core.Plugins;
using Zero.Core.Ribbon;

namespace Zero.App;

public static class Program
{
    public static async Task Main(string[] args)
    {
        var services = new ServiceCollection();

        // Core
        services.AddSingleton<IRibbonProvider, RibbonProvider>();
        services.AddSingleton<ICommandPipeline, LoggingPipeline>();
        services.AddSingleton<ICommandPipeline, ErrorPipeline>();

        // Plugins
        var plugins = PluginLoader.LoadPlugins($"{AppContext.BaseDirectory}//Plugins");
        PluginValidator.ValidateConflicts(plugins);
        var ordered = PluginDependencyResolver.Resolve(plugins);

        foreach (var plugin in ordered)
            plugin.ConfigureServices(services);

        services.AddSingleton<IEnumerable<IPlugin>>(ordered);

        // App + VM
        services.AddSingleton<MainViewModel>();
        services.AddSingleton<MainView>();
        services.AddSingleton<MainWindow>();

        var provider = services.BuildServiceProvider();
        var appBuilder = BuildAvaloniaApp(provider);

        appBuilder.StartWithClassicDesktopLifetime(args);
    }

    public static AppBuilder BuildAvaloniaApp(IServiceProvider provider)
        => AppBuilder.Configure(() => new App(provider))
                     .UsePlatformDetect()
                     .UseSkia()
                     .LogToTrace()
                     .UseReactiveUI(builder => { });
}