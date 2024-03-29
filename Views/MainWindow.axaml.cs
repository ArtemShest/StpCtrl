using Avalonia.Controls;
using Avalonia.ReactiveUI;
using StpCtrl.ViewModels;
using StpCtrl.Models;
using ReactiveUI;
using System;
using System.Threading.Tasks;
using System.Reactive;
using System.Reactive.Disposables;
using Avalonia.Markup.Xaml;
using Avalonia.Collections;

namespace StpCtrl.Views
{
    public partial class MainWindow : ReactiveWindow<MainWindowViewModel>
    {
        public MainWindow()
        {
            InitializeComponent();
            this.WhenActivated(d => d(ViewModel!.CycleDialog.RegisterHandler(ShowCycleDialog)));
            this.WhenActivated(p => p(ViewModel!.SettingsDialog.RegisterHandler(SettingsDialogAsync)));
        }

        private async Task ShowCycleDialog(InteractionContext<CycleViewModel, Stepper> interaction)
        {
            Window dialog = new CycleWindow();

            dialog.DataContext = interaction.Input;

            var result = await dialog.ShowDialog<Stepper>(this);
            interaction.SetOutput(result);
        }

        private async Task SettingsDialogAsync(InteractionContext<SettingsViewModel, Stepper> interaction)
        {
            Window dialog = new SettingsWindow();

            dialog.DataContext = interaction.Input;

            var result = await dialog.ShowDialog<Stepper>(this);
            interaction.SetOutput(result);
        }
    }
}
