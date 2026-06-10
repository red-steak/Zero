using Avalonia;
using Avalonia.Browser;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Zero.App;
using Zero.App.ViewModels;
using Zero.App.Views;
using Zero.Core.Pipelines;
using Zero.Core.Plugins;
using Zero.Core.Ribbon;
using ReactiveUI.Avalonia;

internal sealed partial class Program
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

        await appBuilder.StartBrowserAppAsync("out");
    }

    public static AppBuilder BuildAvaloniaApp(IServiceProvider provider)
        => AppBuilder.Configure(() => new App(provider))
                     .WithInterFont()
                     .LogToTrace()
                     .UseReactiveUI(builder => { });

}