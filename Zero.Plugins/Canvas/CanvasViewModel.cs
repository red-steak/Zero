using ReactiveUI;
using ReactiveUI.SourceGenerators;
using System.Collections.ObjectModel;
using Zero.Core.Commands;
using Zero.Core.Workspaces;

namespace Zero.Plugin.Canvas;

public partial class CanvasViewModel : ReactiveObject, IWorkspaceViewModel
{
    public string Title => "Canvas";

    public CanvasCommands Commands { get; }

    BaseCommandsHolder IWorkspaceViewModel.Commands => Commands;

    public object View { get; }

    public CanvasViewModel(CanvasCommands commands, ObservableCollection<Message> messages)
    {
        Commands = commands;
        Messages = messages;

        View = new CanvasView(this);
    }

    [Reactive] ObservableCollection<Message> messages;

    [Reactive] Message? lastMessage;

    [Reactive] bool isSelected;
}
