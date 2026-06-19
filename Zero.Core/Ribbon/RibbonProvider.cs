using ReactiveUI;
using Zero.Core.Commands;

namespace Zero.Core.Ribbon
{
    public class RibbonProvider : IRibbonProvider
    {
        public IEnumerable<RibbonTabViewModel> BuildRibbon(BaseCommandsHolder holder)
        {
            var shortcuts = holder.GetShortcuts().ToDictionary(x => x.Item1, x => x.Shortcut);

            var commands = holder.GetRibbonCommands(holder.CommandPropertyPairs)
                .Select(c => new
                {
                    c.Command,
                    c.Ribbon.Tab,
                    c.Ribbon.Group,
                    c.Ribbon.Order,
                    c.Ribbon.Label,
                    c.Ribbon.Tooltip,
                    c.Ribbon.Icon,
                    c.Ribbon.IsToggle,
                    c.Ribbon.IsSplit,
                    //KeyTip = shortcuts.TryGetValue(c.Command, out var sc) ? sc.KeyTip : ""
                })
                .ToList();

            return commands
                .GroupBy(c => c.Tab)
                .OrderBy(g => g.Key)
                .Select(tabGroup => new RibbonTabViewModel
                {
                    Header = tabGroup.Key.ToString(),
                    Groups = tabGroup
                        .GroupBy(c => c.Group)
                        .OrderBy(g => g.Key)
                        .Select(group => new RibbonGroupViewModel
                        {
                            Header = group.Key.ToString(),
                            Commands = group
                                .OrderBy(c => c.Order)
                                .Select(c => new RibbonButtonViewModel
                                {
                                    Label = c.Label,
                                    Tooltip = c.Tooltip,
                                    Icon = c.Icon,
                                    IsToggle = c.IsToggle,
                                    IsSplit = c.IsSplit,
                                    //KeyTip = c.KeyTip,
                                    Command = (IReactiveCommand)c.Command
                                })
                                .ToList()
                        })
                        .ToList()
                });
        }

        public void ApplyRibbonSize(IEnumerable<RibbonTabViewModel> tabs, RibbonSize size)
        {
            foreach (var t in tabs)
            {
                t.Size = size;
                foreach (var g in t.Groups)
                {
                    g.Size = size;
                    foreach (var c in g.Commands)
                        c.Size = size;
                }
            }
        }
    }

}
