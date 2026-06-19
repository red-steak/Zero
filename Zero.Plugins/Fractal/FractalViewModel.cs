using ReactiveUI;
using ReactiveUI.SourceGenerators;
using System.Collections.ObjectModel;
using Zero.Core.Commands;
using Zero.Core.Workspaces;

namespace Zero.Plugin.Fractal
{
    public partial class FractalViewModel : ReactiveObject, IWorkspaceViewModel
    {
        public string Title => "Fractal";

        public FractalCommands Commands { get; }

        BaseCommandsHolder IWorkspaceViewModel.Commands => Commands;

        public object View { get; }

        public FractalViewModel(FractalCommands commands, ObservableCollection<Message> messages)
        {
            Commands = commands;
            Messages = messages;

            View = new FractalView(this);
        }

        [Reactive] ObservableCollection<Message> messages;

        [Reactive] Message? lastMessage;

        [Reactive] bool isSelected;
    }
}
