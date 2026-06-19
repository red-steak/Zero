using ReactiveUI;
using ReactiveUI.SourceGenerators;
using System.Collections.ObjectModel;
using Zero.Core.Commands;
using Zero.Core.Workspaces;

namespace Zero.Plugin.Shared
{
    public partial class SharedViewModel : ReactiveObject, IWorkspaceViewModel
    {
        public string Title => "Zero: Shared";

        public SharedCommands Commands { get; }

        BaseCommandsHolder IWorkspaceViewModel.Commands => Commands;

        public object View { get; }

        public SharedViewModel(SharedCommands commands, ObservableCollection<Message> messages)
        {
            Commands = commands;
            Messages = messages;

            View = new SharedView(this);
        }

        [Reactive] ObservableCollection<Message> messages;

        [Reactive] Message? lastMessage;

        [Reactive] bool isSelected;
    }
}
