namespace Zero.Core.Plugins
{
    public class PluginDependency
    {
        public string Name { get; set; } = "";
        public string MinVersion { get; set; } = "1.0.0";
        public bool Optional { get; set; }
    }
}
