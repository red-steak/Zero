using System.Reflection;

namespace Zero.Core.Plugins
{
    public static class PluginLoader
    {
        public static List<IPlugin> LoadPlugins(string folder)
        {
            var plugins = new List<IPlugin>();

            foreach (var file in Directory.GetFiles(folder, "Zero.Plugin.*.dll"))
            {
                var asm = Assembly.LoadFrom(file);

                foreach (var type in asm.GetTypes())
                {
                    if (typeof(IPlugin).IsAssignableFrom(type) && !type.IsAbstract)
                    {
                        plugins.Add((IPlugin)Activator.CreateInstance(type)!);
                    }
                }
            }

            return plugins;
        }
    }

}
