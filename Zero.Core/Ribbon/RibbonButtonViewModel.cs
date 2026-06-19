using ReactiveUI;
using ReactiveUI.SourceGenerators;


namespace Zero.Core.Ribbon
{
    public partial class RibbonButtonViewModel : ReactiveObject
    {
        [Reactive] string label = "";
        [Reactive] string tooltip = "";
        [Reactive] string icon = "";
        [Reactive] bool isToggle;
        [Reactive] bool isSplit;
        [Reactive] string keyTip = "";
        [Reactive] RibbonSize size = RibbonSize.Large;
        [Reactive] IReactiveCommand command = default!;
    }
}
