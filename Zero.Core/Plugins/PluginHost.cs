using Microsoft.Extensions.DependencyInjection;

namespace Zero.Core.Plugins
{
    public class PluginHost
    {
        private readonly IServiceCollection _services;
        private readonly List<IPlugin> _plugins = new();

        public PluginHost(IServiceCollection services)
        {
            _services = services;
        }

        public void LoadInitial(string folder)
        {
            var plugins = PluginLoader.LoadPlugins(folder);
            PluginValidator.ValidateConflicts(plugins);
            var ordered = PluginDependencyResolver.Resolve(plugins);

            foreach (var p in ordered)
            {
                p.ConfigureServices(_services);
                _plugins.Add(p);
            }
        }

        // pro hot reload bys měl pluginy loadovat do vlastního AssemblyLoadContextu
    }

}
