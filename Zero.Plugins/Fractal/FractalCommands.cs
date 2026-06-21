using Avalonia.Threading;
using ReactiveUI;
using ReactiveUI.SourceGenerators;
using System.Collections.ObjectModel;
using Zero.Core.Commands;
using Zero.Core.Pipelines;
using Zero.Core.Ribbon;
using Zero.Core.Workspaces;

namespace Zero.Plugin.Fractal
{
    public partial class FractalCommands(IEnumerable<ICommandPipeline> pipelines, ObservableCollection<Message> messages) : BaseCommandsHolder(pipelines, messages)
    {
        [Ribbon(
            tab: RibbonTab.Home,
            group: RibbonGroup.File,
            order: 0,
            label: "Load Fractal",
            tooltip: "Load Fractal from server",
            icon: "download",
            isToggle: false,
            isSplit: false
        )]
        [Command("3fd5fd1d-ab63-452b-bd66-f184bac5ef1e", CommandKind.Load)]
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

        [Ribbon(RibbonTab.Home, RibbonGroup.File, 1, "Upload Fractal", "Upload Fractal", "upload")]
        [Command("83039b8a-871d-425b-bdaa-0face24aa720", CommandKind.Save)]
        [NoExecCommands([])]
        [ReactiveCommand]
        async Task<bool> Save(string json)
        {
            await Task.Delay(5200);
            return true;
        }

        [Ribbon(RibbonTab.Home, RibbonGroup.File, 1, "Format Fractal", "Format Fractal", "format")]
        [Command("a11908d4-581a-422d-a2a8-c9f307460da9", CommandKind.Format)]
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
            label: "Load Fractal",
            tooltip: "Load Fractal file from server",
            icon: "download",
            isToggle: false,
            isSplit: false
        )]
        [Command("a20efef5-293e-4bce-b768-8311c57c11b4", CommandKind.Load)]
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

        [Ribbon(RibbonTab.Home, RibbonGroup.Custom, 1, "Upload Fractal", "Upload Fractal", "upload")]
        [Command("11da6e3b-5d1b-410c-bc1f-fe0197524ca3", CommandKind.Save)]
        [NoExecCommands([])]
        [ReactiveCommand]
        async Task<bool> CustomSave(string json)
        {
            await Task.Delay(5200);
            return true;
        }

        [Ribbon(RibbonTab.Home, RibbonGroup.File, 1, "Format Fractal", "Format Fractal", "format")]
        [Command("a11908d4-581a-422d-a2a8-c9f307460da9", CommandKind.Format)]
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
        [Command("b3897c75-e628-4703-8115-8de547558791", CommandKind.Search)]
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
        [Command("46232f73-cfff-4a26-b0c5-fa970b57ace7", CommandKind.Format)]
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