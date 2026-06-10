using DynamicData;
using DynamicData.Binding;
using Microsoft.Extensions.DependencyInjection;
using ReactiveUI;
using ReactiveUI.SourceGenerators;
using Splat;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reactive;
using System.Reactive.Linq;
using Zero.Core.Commands;
using Zero.Core.Plugins;
using Zero.Core.Ribbon;
using Zero.Core.Workspaces;

namespace Zero.App.ViewModels;

public partial class MainViewModel : ReactiveObject
{
    [Reactive] ObservableCollection<IWorkspaceViewModel> workspaces = [];

    [Reactive] IWorkspaceViewModel? selectedWorkspace;
    [Reactive] ObservableCollection<RibbonTabViewModel> ribbonTabs = [];

    [Reactive] double windowWidth = 1000;
    [Reactive] double windowHeight = 600;
    [Reactive] RibbonSize ribbonSize = RibbonSize.Large;

    public ReactiveCommand<IWorkspaceViewModel, Unit> SelectWorkspace { get; init; }

    public ReactiveCommand<string, Unit> GlobalHotkeyCommand { get; }

    private readonly IRibbonProvider _ribbonProvider;

    public MainViewModel(IEnumerable<IPlugin> plugins, IRibbonProvider ribbonProvider)
    {
        _ribbonProvider = ribbonProvider;

        Workspaces.Add(plugins.Select(x => x.CreateWorkspace()));

        SelectWorkspace = ReactiveCommand.Create<IWorkspaceViewModel>(workspace => 
        {
            if (SelectedWorkspace is not null && SelectedWorkspace != workspace)
            {
                SelectedWorkspace.IsSelected = false;
                SelectedWorkspace = workspace;
                workspace.IsSelected = true;
            }
        });

        if (Workspaces.Any())
        {
            SelectedWorkspace = Workspaces[0];
            SelectedWorkspace.IsSelected = true;
        }


        this.WhenAnyValue(x => x.SelectedWorkspace)
            .Subscribe(ws =>
            {
                if (RibbonTabs.Any()) RibbonTabs.RemoveMany(RibbonTabs);

                if (ws is not null)
                {
                    ws.Commands.InitializeCommands();
                    var ribbonvms = _ribbonProvider.BuildRibbon(ws.Commands);
                    foreach (var ribbonvm in ribbonvms)
                    {
                        RibbonTabs.Add(ribbonvm);
                    }

                    ws.Messages
                    .ToObservableChangeSet()
                    .Subscribe(_ =>
                    {
                        ws.LastMessage = ws.Messages.LastOrDefault();
                    });
                }
            });

        this.WhenAnyValue(x => x.WindowWidth, x => x.WindowHeight, x => x.SelectedWorkspace)
            .Subscribe(tuple =>
            {
                var (w, h, selectedWorkspace) = tuple;
                if (w < 400 || h < 300) RibbonSize = RibbonSize.Small;
                else if (w < 800 || h < 600) RibbonSize = RibbonSize.Medium;
                else RibbonSize = RibbonSize.Large;

                _ribbonProvider.ApplyRibbonSize(RibbonTabs, RibbonSize);
            });

        GlobalHotkeyCommand = ReactiveCommand.Create<string>(gesture =>
        {
            if (SelectedWorkspace?.Commands is not BaseCommandsHolder holder)
                return;

            //foreach (var (cmd, shortcut) in holder.GetShortcuts())
            //{
            //    if (shortcut.Gesture == gesture && ((ICommand)cmd).CanExecute(null))
            //    {
            //        ((ICommand)cmd).Execute(null);
            //        return;
            //    }
            //}
        });

    }
}
