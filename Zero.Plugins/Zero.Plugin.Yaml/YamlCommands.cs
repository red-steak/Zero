using Avalonia.Threading;
using ReactiveUI;
using ReactiveUI.SourceGenerators;
using System.Collections.ObjectModel;
using System.Windows.Input;
using Zero.Core.Commands;
using Zero.Core.Pipelines;
using Zero.Core.Ribbon;
using Zero.Core.Workspaces;

namespace Zero.Plugin.Yaml;

public partial class YamlCommands(IEnumerable<ICommandPipeline> pipelines, ObservableCollection<Message> messages) : BaseCommandsHolder(pipelines)
{
    ICollection<Message> _messages = messages;

    public void WriteLog(Guid id, MessageKind kind, IReactiveCommand command, string text)
    {
        Dispatcher.UIThread.InvokeAsync(async () => 
        {

            var m = _messages.LastOrDefault(m => m.Id.Equals(id));
            if (m == null)
            {
                var lastMessage = new Message(command, id) { Kind = kind, Text = text };
                _messages.Add(lastMessage);
            }
            else
            {
                m.Kind = kind;
                m.Text = text;
            }
        });
    }


    [Ribbon(
        tab: RibbonTab.Home,
        group: RibbonGroup.File,
        order: 0,
        label: "Load YAML",
        tooltip: "Load YAML file from server",
        icon: "download",
        isToggle: false,
        isSplit: false
    )]
    [Command("{F0112EE6-14AF-4AFD-8B2B-2EEFCD242D8E}", CommandKind.Load)]
    [NoExecCommands([])]
    [ReactiveCommand]
    async Task<string> Load()
    {
        Guid id = Guid.NewGuid();

        WriteLog(id, MessageKind.Loading, LoadCommand, "Loading data...");
        await Task.Delay(4200);
        WriteLog(id, MessageKind.Working, LoadCommand, "Parsing data...");
        await Task.Delay(1600);
        var yamlText = "name: Zero\nversion: 1.0.0\nitems:\n  - one\n  - two";
        WriteLog(id, MessageKind.Success, LoadCommand, "Loaded!");
        
        return yamlText;
    }

    [Ribbon(RibbonTab.Home, RibbonGroup.File, 1, "Upload YAML", "Upload YAML", "upload")]
    [Command("{53DF43B0-F4CB-4E91-9C35-C67AB5CB356F}", CommandKind.Save)]
    [NoExecCommands([])]
    [ReactiveCommand]
    async Task<bool> Save(string json)
    {            
        await Task.Delay(5200);
        return true;
    }

    [Ribbon(RibbonTab.Home, RibbonGroup.File, 1, "Format YAML", "Format YAML", "format")]
    [Command("{7D46A8B6-7630-4A50-BAB6-92995763EABB}", CommandKind.Format)]
    [NoExecCommands([])]
    [ReactiveCommand]
    async Task<string> Format(string text)
    {
        await Task.Delay(5200);
        return "Who knows something???";
    }
}