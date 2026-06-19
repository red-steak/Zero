using ReactiveUI;
using ReactiveUI.SourceGenerators;

namespace Zero.Core.Ribbon
{
    public partial class RibbonTabViewModel : ReactiveObject
    {
        [Reactive] string header = "";
        [Reactive] List<RibbonGroupViewModel> groups = [];
        [Reactive] RibbonSize size = RibbonSize.Large;
    }
}
