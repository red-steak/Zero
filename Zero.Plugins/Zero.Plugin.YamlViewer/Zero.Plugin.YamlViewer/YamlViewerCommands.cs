using ReactiveUI;
using ReactiveUI.SourceGenerators;
using YamlDotNet.Serialization;
using YamlDotNet.Serialization.NamingConventions;
using Zero.Core.Commands;
using Zero.Core.Pipelines;
using Zero.Core.Ribbon;

namespace Zero.Plugin.YamlViewer;

public partial class YamlViewerCommands(IEnumerable<ICommandPipeline> pipelines) : BaseCommandsHolder(pipelines)
{
    [Ribbon(RibbonTab.Tools, RibbonGroup.Editing, 0, "Tool for YAML", "Tool for YAML", "tool")]
    [Command("{3E776140-BD9A-4633-994B-A7F8C3A3416A}", CommandKind.Specific)]
    [NoExecCommands([])]
    [ReactiveCommand]
    async Task<string> Tool(string text)
    {
        await Task.Delay(3200);

        var deserializer = new DeserializerBuilder()
            .WithNamingConvention(CamelCaseNamingConvention.Instance)
            .Build();

        var serializer = new SerializerBuilder()
            .WithIndentedSequences()
            .WithNamingConvention(CamelCaseNamingConvention.Instance)
            .Build();

        try
        {
            var obj = deserializer.Deserialize<object>(text);
            var yamlText = serializer.Serialize(obj);
            return yamlText; 
        }
        catch
        {
            // ignore formatting errors
        }


        return string.Empty;
    }

}