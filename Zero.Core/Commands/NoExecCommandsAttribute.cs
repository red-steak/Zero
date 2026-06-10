namespace Zero.Core.Commands
{
    [AttributeUsage(AttributeTargets.Method)]
    public class NoExecCommandsAttribute(params CommandKind[] kinds) : Attribute
    {
        public CommandKind[] ForbiddenKinds { get; } = kinds;
    }
}
