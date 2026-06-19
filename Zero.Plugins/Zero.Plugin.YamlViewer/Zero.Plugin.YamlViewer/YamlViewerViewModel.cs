using ReactiveUI;
using ReactiveUI.SourceGenerators;
using System.Collections.ObjectModel;
using Zero.Core.Commands;
using Zero.Core.Workspaces;

namespace Zero.Plugin.YamlViewer;

public partial class YamlViewerViewModel : ReactiveObject, IWorkspaceViewModel
{
    public string Title => "YAML Viewer";

    public YamlViewerCommands Commands { get; }

    BaseCommandsHolder IWorkspaceViewModel.Commands => Commands;

    public object View { get; }

    public YamlViewerViewModel(YamlViewerCommands commands)
    {
        Messages = new ObservableCollection<Message>();
        Commands = commands;
        View = new YamlViewerView(this);
    }

    // Data bound to the editor
    private string _yamlText = "";
    public string YamlText
    {
        get => _yamlText;
        set => this.RaiseAndSetIfChanged(ref _yamlText, value);
    }

    [Reactive] ObservableCollection<Message> messages;

    [Reactive] Message? lastMessage;

    [Reactive] bool isSelected;
}
