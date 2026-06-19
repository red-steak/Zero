using ReactiveUI;
using ReactiveUI.SourceGenerators;

namespace Zero.Core.Ribbon
{
    public partial class RibbonGroupViewModel : ReactiveObject
    {
        [Reactive] string header = "";
        public List<RibbonButtonViewModel> Commands { get; init; } = [];
        [Reactive] RibbonSize size = RibbonSize.Large;
    }
}
