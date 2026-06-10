namespace Zero.Core.Plugins
{
    public class PluginManifest
    {
        public string Name { get; set; } = "";
        public string Version { get; set; } = "1.0.0";
        public List<PluginDependency> Dependencies { get; set; } = new();
        public List<string> Conflicts { get; set; } = new();
    }
}
