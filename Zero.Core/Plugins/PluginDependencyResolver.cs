namespace Zero.Core.Plugins
{
    public static class PluginDependencyResolver
    {
        public static List<IPlugin> Resolve(List<IPlugin> plugins)
        {
            var nameToPlugin = plugins.ToDictionary(p => p.Manifest.Name);
            var graph = new Dictionary<string, List<string>>();
            var indegree = new Dictionary<string, int>();

            foreach (var p in plugins)
            {
                graph[p.Manifest.Name] = new List<string>();
                indegree[p.Manifest.Name] = 0;
            }

            foreach (var plugin in plugins)
            {
                foreach (var dep in plugin.Manifest.Dependencies.Where(d => !d.Optional))
                {
                    if (!nameToPlugin.ContainsKey(dep.Name))
                        throw new Exception($"Missing dependency: {plugin.Manifest.Name} → {dep.Name}");

                    var depPlugin = nameToPlugin[dep.Name];
                    if (Version.Parse(dep.MinVersion) > Version.Parse(depPlugin.Manifest.Version))
                        throw new Exception($"Plugin {plugin.Manifest.Name} requires {dep.Name} >= {dep.MinVersion}");

                    graph[dep.Name].Add(plugin.Manifest.Name);
                    indegree[plugin.Manifest.Name]++;
                }
            }

            var queue = new Queue<string>(indegree.Where(kv => kv.Value == 0).Select(kv => kv.Key));
            var result = new List<IPlugin>();

            while (queue.Count > 0)
            {
                var name = queue.Dequeue();
                result.Add(nameToPlugin[name]);

                foreach (var next in graph[name])
                {
                    indegree[next]--;
                    if (indegree[next] == 0)
                        queue.Enqueue(next);
                }
            }

            if (result.Count != plugins.Count)
                throw new Exception("Circular dependency detected!");

            return result;
        }
    }

}
