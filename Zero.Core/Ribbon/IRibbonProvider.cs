using Zero.Core.Commands;

namespace Zero.Core.Ribbon
{
    public interface IRibbonProvider
    {
        IEnumerable<RibbonTabViewModel> BuildRibbon(BaseCommandsHolder holder);
        void ApplyRibbonSize(IEnumerable<RibbonTabViewModel> tabs, RibbonSize size);
    }
}
