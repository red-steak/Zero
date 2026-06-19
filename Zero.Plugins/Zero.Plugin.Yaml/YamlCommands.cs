using Avalonia.Threading;
using ReactiveUI;
using ReactiveUI.SourceGenerators;
using System.Collections.ObjectModel;
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


    [Ribbon(
        tab: RibbonTab.Home,
        group: RibbonGroup.Custom,
        order: 0,
        label: "Load YAML",
        tooltip: "Load YAML file from server",
        icon: "download",
        isToggle: false,
        isSplit: false
    )]
    [Command("{20112EE6-14AF-4AFD-8B2B-2EEFCD242D8E}", CommandKind.Load)]
    [NoExecCommands([])]
    [ReactiveCommand]
    async Task<string> CustomLoad()
    {
        Guid id = Guid.NewGuid();

        WriteLog(id, MessageKind.Loading, LoadCommand, "CUSTOM loading data...");
        await Task.Delay(4200);
        WriteLog(id, MessageKind.Working, LoadCommand, "Parsing CUSTOM data...");
        await Task.Delay(1600);
        var yamlText = "name: Zero\nversion: 1.0.0\nitems:\n  - one\n  - two";
        WriteLog(id, MessageKind.Success, LoadCommand, "CUSTOM data loaded!");

        return yamlText;
    }

    [Ribbon(RibbonTab.Home, RibbonGroup.Custom, 1, "Upload YAML", "Upload YAML", "upload")]
    [Command("{23DF43B0-F4CB-4E91-9C35-C67AB5CB356F}", CommandKind.Save)]
    [NoExecCommands([])]
    [ReactiveCommand]
    async Task<bool> CustomSave(string json)
    {
        await Task.Delay(5200);
        return true;
    }

    [Ribbon(RibbonTab.Home, RibbonGroup.File, 1, "Format YAML", "Format YAML", "format")]
    [Command("{2D46A8B6-7630-4A50-BAB6-92995763EABB}", CommandKind.Format)]
    [NoExecCommands([])]
    [ReactiveCommand]
    async Task<string> CustomFormat(string text)
    {
        await Task.Delay(5200);
        return "Who knows something???";
    }




    [Ribbon(
    tab: RibbonTab.Tools,
    group: RibbonGroup.Search,
    order: 0,
    label: "Search",
    tooltip: "Search for something......",
    icon: "search",
    isToggle: false,
    isSplit: false
)]
    [Command("{18001CC1-532F-484F-9CF0-2E086F2237E2}", CommandKind.Search)]
    [NoExecCommands([])]
    [ReactiveCommand]
    async Task<int> Search()
    {
        Guid id = Guid.NewGuid();

        WriteLog(id, MessageKind.Working, SearchCommand, "Seraching for item ...");
        await Task.Delay(4200);
        WriteLog(id, MessageKind.Working, SearchCommand, "Validating data...");
        await Task.Delay(1600);
        WriteLog(id, MessageKind.Success, SearchCommand, "Found!");

        return 1;
    }

    [Ribbon(
    tab: RibbonTab.Tools,
    group: RibbonGroup.Format,
    order: 1,
    label: "Format",
    tooltip: "Format something......",
    icon: "format",
    isToggle: false,
    isSplit: false
)]
    [Command("{5B0FF331-E04D-4224-B7F3-7E9155025DFE}", CommandKind.Format)]
    [NoExecCommands([])]
    [ReactiveCommand]
    async Task<string> FormatData()
    {
        Guid id = Guid.NewGuid();

        WriteLog(id, MessageKind.Working, FormatDataCommand, "Formating data XYZ ...");
        await Task.Delay(4200);
        WriteLog(id, MessageKind.Working, FormatDataCommand, "Validating data...");
        await Task.Delay(1600);
        WriteLog(id, MessageKind.Success, FormatDataCommand, "Formated!");

        return "Cao WORLD!";
    }




}