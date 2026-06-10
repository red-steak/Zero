using ReactiveUI;
using Zero.Core.Commands;

namespace Zero.Core.Pipelines
{
    public class CommandContext(object holder, IReactiveCommand command, CommandKind kind, object? parameter)
    {
        public object Holder { get; } = holder;
        public IReactiveCommand Command { get; } = command;
        public CommandKind Kind { get; } = kind;
        public object? Parameter { get; } = parameter;
    }
}
