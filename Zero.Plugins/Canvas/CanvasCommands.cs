using Avalonia.Threading;
using ReactiveUI;
using ReactiveUI.SourceGenerators;
using System.Collections.ObjectModel;
using Zero.Core.Commands;
using Zero.Core.Pipelines;
using Zero.Core.Ribbon;
using Zero.Core.Workspaces;

namespace Zero.Plugin.Canvas;

public partial class CanvasCommands(IEnumerable<ICommandPipeline> pipelines, ObservableCollection<Message> messages) : BaseCommandsHolder(pipelines, messages)
{
    [Ribbon(
        tab: RibbonTab.Home,
        group: RibbonGroup.File,
        order: 0,
        label: "Load Canvas",
        tooltip: "Load Canvas from server",
        icon: "download",
        isToggle: false,
        isSplit: false
    )]
    [Command("ea929a6c-b48a-449d-9a1d-0a03be061bf9", CommandKind.Load)]
    [NoExecCommands([])]
    [ReactiveCommand]
    async Task<string> Load()
    {
        Guid id = Guid.NewGuid();

        WriteLog(id, MessageKind.Loading, LoadCommand, "Loading data...");
        await Task.Delay(4200);
        WriteLog(id, MessageKind.Working, LoadCommand, "Parsing data...");
        await Task.Delay(1600);
        var text = "name: Zero\nversion: 1.0.0\nitems:\n  - one\n  - two";
        WriteLog(id, MessageKind.Success, LoadCommand, "Loaded!");

        return text;
    }

    [Ribbon(RibbonTab.Home, RibbonGroup.File, 1, "Upload Canvas", "Upload Canvas", "upload")]
    [Command("f953f780-8de4-47fc-89a2-d6428f14ccd3", CommandKind.Save)]
    [NoExecCommands([])]
    [ReactiveCommand]
    async Task<bool> Save(string json)
    {
        await Task.Delay(5200);
        return true;
    }

    [Ribbon(RibbonTab.Home, RibbonGroup.File, 1, "Format Canvas", "Format Canvas", "format")]
    [Command("64a94cdd-0d79-4d86-9a15-c19d38668abd", CommandKind.Format)]
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
        label: "Load Canvas",
        tooltip: "Load Canvas file from server",
        icon: "download",
        isToggle: false,
        isSplit: false
    )]
    [Command("f8f8e0f8-362e-4c03-956f-b55d94d5fb16", CommandKind.Load)]
    [NoExecCommands([])]
    [ReactiveCommand]
    async Task<string> CustomLoad()
    {
        Guid id = Guid.NewGuid();

        WriteLog(id, MessageKind.Loading, LoadCommand, "CUSTOM loading data...");
        await Task.Delay(4200);
        WriteLog(id, MessageKind.Working, LoadCommand, "Parsing CUSTOM data...");
        await Task.Delay(1600);
        var text = "name: Zero\nversion: 1.0.0\nitems:\n  - one\n  - two";
        WriteLog(id, MessageKind.Success, LoadCommand, "CUSTOM data loaded!");

        return text;
    }

    [Ribbon(RibbonTab.Home, RibbonGroup.Custom, 1, "Upload Canvas", "Upload Canvas", "upload")]
    [Command("f82d5e9e-157c-4242-abcf-822798431d85", CommandKind.Save)]
    [NoExecCommands([])]
    [ReactiveCommand]
    async Task<bool> CustomSave(string json)
    {
        await Task.Delay(5200);
        return true;
    }

    [Ribbon(RibbonTab.Home, RibbonGroup.File, 1, "Format Canvas", "Format Canvas", "format")]
    [Command("64a94cdd-0d79-4d86-9a15-c19d38668abd", CommandKind.Format)]
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
    [Command("6544ab29-cada-4a36-bf18-0553af538106", CommandKind.Search)]
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
    [Command("2e08402a-889c-4f68-98f1-f0ffbb42c253", CommandKind.Format)]
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