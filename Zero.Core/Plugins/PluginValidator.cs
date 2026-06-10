namespace Zero.Core.Plugins
{
    public static class PluginValidator
    {
        public static void ValidateConflicts(List<IPlugin> plugins)
        {
            var names = plugins.Select(p => p.Manifest.Name).ToHashSet();
            foreach (var p in plugins)
            {
                foreach (var conflict in p.Manifest.Conflicts)
                {
                    if (names.Contains(conflict))
                        throw new Exception($"Plugin {p.Manifest.Name} conflicts with {conflict}");
                }
            }
        }
    }

}
