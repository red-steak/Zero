using Microsoft.Extensions.DependencyInjection;
using Splat;
using Zero.Core.Plugins;
using Zero.Core.Workspaces;

namespace Zero.Plugin.YamlViewer;

public class YamlViewerPlugin : IPlugin
{
    IServiceCollection? _services;

    public PluginManifest Manifest { get; } = new PluginManifest
    {
        Name = "Zero.Plugin.YamlViewer",
        Version = "1.0.0",
        Dependencies = [],
        Conflicts = []
    };

    public void ConfigureServices(IServiceCollection services)
    {
        _services = services;
        services.AddSingleton<YamlViewerCommands>();
        services.AddSingleton<YamlViewerViewModel>();
    }

    public IWorkspaceViewModel CreateWorkspace()
    {
        if (_services is null)
            throw new System.InvalidOperationException("Services not configured. Call ConfigureServices first.");

        var provider = _services.BuildServiceProvider();
        return provider.GetRequiredService<YamlViewerViewModel>();
    }

}