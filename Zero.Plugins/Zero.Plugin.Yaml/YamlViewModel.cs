using ReactiveUI;
using ReactiveUI.SourceGenerators;
using System.Collections.ObjectModel;
using Zero.Core.Commands;
using Zero.Core.Workspaces;

namespace Zero.Plugin.Yaml;

public partial class YamlViewModel : ReactiveObject, IWorkspaceViewModel
{
    public string Title => "YAML Editor";

    public YamlCommands Commands { get; }

    BaseCommandsHolder IWorkspaceViewModel.Commands => Commands;

    public object View { get; }

    public YamlViewModel(YamlCommands commands, ObservableCollection<Message> messages)
    {
        Commands = commands;
        Messages = messages;

        View = new YamlView(this);
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
