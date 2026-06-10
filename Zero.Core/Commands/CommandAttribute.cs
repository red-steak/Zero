namespace Zero.Core.Commands
{
    [AttributeUsage(AttributeTargets.Method)]
    public class CommandAttribute(string id, CommandKind kind) : Attribute
    {
        public string Id { get; } = id;
        public CommandKind Kind { get; } = kind;
    }
}
