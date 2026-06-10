using Microsoft.Extensions.DependencyInjection;
using Zero.Core.Workspaces;

namespace Zero.Core.Plugins
{
    public interface IPlugin
    {
        PluginManifest Manifest { get; }

        void ConfigureServices(IServiceCollection services);
        IWorkspaceViewModel CreateWorkspace();
    }

}
