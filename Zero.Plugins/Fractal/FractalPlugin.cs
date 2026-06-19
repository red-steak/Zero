using Microsoft.Extensions.DependencyInjection;
using System.Collections.ObjectModel;
using Zero.Core.Plugins;
using Zero.Core.Workspaces;

namespace Zero.Plugin.Fractal
{
    public class FractalPlugin : IPlugin
    {
        IServiceCollection? _services;

        public PluginManifest Manifest { get; } = new PluginManifest
        {
            Name = "Fractal",
            Version = "0.0.0",
            Dependencies = new(),
            Conflicts = new()
        };

        public void ConfigureServices(IServiceCollection services)
        {
            _services = services;
            services.AddSingleton<ObservableCollection<Message>>([]);
            services.AddSingleton<FractalCommands>();
            services.AddSingleton<FractalViewModel>();
        }

        public IWorkspaceViewModel CreateWorkspace()
        {
            if (_services is null)
                throw new System.InvalidOperationException("Services not configured. Call ConfigureServices first.");

            var provider = _services.BuildServiceProvider();
            return provider.GetRequiredService<FractalViewModel>();
        }

    }
}