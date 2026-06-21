using Avalonia.Threading;
using ReactiveUI;
using ReactiveUI.SourceGenerators;
using System.Collections.ObjectModel;
using Zero.Core.Commands;
using Zero.Core.Pipelines;
using Zero.Core.Ribbon;
using Zero.Core.Workspaces;

namespace Zero.Plugin.Shared
{
    public partial class SharedCommands(IEnumerable<ICommandPipeline> pipelines, ObservableCollection<Message> messages) : BaseCommandsHolder(pipelines, messages)
    {

        [Ribbon(
            tab: RibbonTab.Home,
            group: RibbonGroup.File,
            order: 0,
            label: "Load Shared",
            tooltip: "Load Shared from server",
            icon: "download",
            isToggle: false,
            isSplit: false
        )]
        [Command("a5df606c-8aad-4bb6-9db8-f90e363b5039", CommandKind.Load)]
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

        [Ribbon(RibbonTab.Home, RibbonGroup.File, 1, "Upload Shared", "Upload Shared", "upload")]
        [Command("29d5c520-c0da-490c-8db1-2f247f861431", CommandKind.Save)]
        [NoExecCommands([])]
        [ReactiveCommand]
        async Task<bool> Save(string json)
        {
            await Task.Delay(5200);
            return true;
        }

        [Ribbon(RibbonTab.Home, RibbonGroup.File, 1, "Format Shared", "Format Shared", "format")]
        [Command("0757cfcd-1003-4e1a-9b7f-04e887739068", CommandKind.Format)]
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
            label: "Load Shared",
            tooltip: "Load Shared file from server",
            icon: "download",
            isToggle: false,
            isSplit: false
        )]
        [Command("48fe440c-6a9d-4a4a-b84a-2ab398863917", CommandKind.Load)]
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

        [Ribbon(RibbonTab.Home, RibbonGroup.Custom, 1, "Upload Shared", "Upload Shared", "upload")]
        [Command("173d6741-4f13-4d7f-94d4-ca4fa7a08979", CommandKind.Save)]
        [NoExecCommands([])]
        [ReactiveCommand]
        async Task<bool> CustomSave(string json)
        {
            await Task.Delay(5200);
            return true;
        }

        [Ribbon(RibbonTab.Home, RibbonGroup.File, 1, "Format Shared", "Format Shared", "format")]
        [Command("0757cfcd-1003-4e1a-9b7f-04e887739068", CommandKind.Format)]
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
        [Command("6745e780-88d4-4b3c-abf1-f51ad7f2fb22", CommandKind.Search)]
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
        [Command("02b8b8cb-659d-448c-affe-c18b53ae580b", CommandKind.Format)]
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
}