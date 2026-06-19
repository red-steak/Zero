using System.Reflection;

namespace Zero.Core.Commands
{
    public record struct CommandPropertyPair(MethodInfo M, PropertyInfo P)
    {
        public static implicit operator (MethodInfo M, PropertyInfo P)(CommandPropertyPair value)
        {
            return (value.M, value.P);
        }

        public static implicit operator CommandPropertyPair((MethodInfo M, PropertyInfo P) value)
        {
            return new CommandPropertyPair(value.M, value.P);
        }
    }
}
