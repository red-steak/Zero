using System.Collections.ObjectModel;
using Zero.Core.Commands;

namespace Zero.Core.Workspaces
{
    public interface IWorkspaceViewModel
    {
        string Title { get; }
        BaseCommandsHolder Commands { get; }
        object View { get; }
        bool IsSelected { get; set; }

        ObservableCollection<Message> Messages { get; set; }

        Message? LastMessage { get; set; }
    }

}
