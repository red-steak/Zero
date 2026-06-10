namespace Zero.Core.KeyboardShortcuts
{
    [AttributeUsage(AttributeTargets.Property)]
    public class KeyboardShortcutAttribute(string gesture, string keyTip) : Attribute
    {
        public string Gesture { get; } = gesture;
        public string KeyTip { get; } = keyTip;
    }
}
