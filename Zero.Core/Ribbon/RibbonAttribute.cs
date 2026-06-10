namespace Zero.Core.Ribbon
{
    [AttributeUsage(AttributeTargets.Method)]
    public class RibbonAttribute(
        RibbonTab tab,
        RibbonGroup group,
        int order,
        string label,
        string tooltip,
        string icon,
        bool isToggle = false,
        bool isSplit = false) : Attribute
    {
        public RibbonTab Tab { get; } = tab;
        public RibbonGroup Group { get; } = group;
        public int Order { get; } = order;
        public string Label { get; } = label;
        public string Tooltip { get; } = tooltip;
        public string Icon { get; } = icon;
        public bool IsToggle { get; } = isToggle;
        public bool IsSplit { get; } = isSplit;
    }
}
