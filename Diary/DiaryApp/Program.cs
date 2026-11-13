using Avalonia;
using DiaryApp.Servicies;
using DiaryApp.ViewModels;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace DiaryApp;

sealed class Program
{
    // Správně typu IServiceProvider
    public static IServiceProvider Services { get; private set; } = null!;

    [STAThread]
    public static void Main(string[] args)
    {
        var services = new ServiceCollection();

        ConfigureServices(services);

        // Sestavení DI kontejneru PŘED spuštěním aplikace
        Services = services.BuildServiceProvider();

        BuildAvaloniaApp()
            .StartWithClassicDesktopLifetime(args);
    }

    public static void ConfigureServices(IServiceCollection services)
    {
        services.AddSingleton<ISettingsService, SettingsService>();
        services.AddSingleton<IFilesService, FilesService>();
        services.AddTransient<MainViewModel>();
    }

    // Avalonia configuration
    public static AppBuilder BuildAvaloniaApp()
        => AppBuilder.Configure<App>()
            .UsePlatformDetect()
#if DEBUG
            .WithDeveloperTools()
#endif
            .WithInterFont()
            .LogToTrace();
}