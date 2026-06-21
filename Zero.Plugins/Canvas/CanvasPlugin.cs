using Microsoft.Extensions.DependencyInjection;
using System.Collections.ObjectModel;
using Zero.Core.Plugins;
using Zero.Core.Workspaces;

namespace Zero.Plugin.Canvas;

public class CanvasPlugin : IPlugin
{
    IServiceCollection? _services;

    public PluginManifest Manifest { get; } = new PluginManifest
    {
        Name = "Canvas",
        Version = "0.0.0",
        Dependencies = [],
        Conflicts = []
    };

    public void ConfigureServices(IServiceCollection services)
    {
        _services = services;
        services.AddSingleton<ICollection<Message>>(new ObservableCollection<Message>());
        services.AddSingleton<CanvasCommands>();
        services.AddSingleton<CanvasViewModel>();
    }

    public IWorkspaceViewModel CreateWorkspace()
    {
        if (_services is null)
            throw new System.InvalidOperationException("Services not configured. Call ConfigureServices first.");

        var provider = _services.BuildServiceProvider();
        return provider.GetRequiredService<CanvasViewModel>();
    }

}