using Avalonia.Controls;
using Avalonia.ReactiveUI;
using ReactiveUI;
using StpCtrl.ViewModels;
using System;

namespace StpCtrl.Views
{
    public partial class SettingsWindow : ReactiveWindow<SettingsViewModel>
    {
        public SettingsWindow()
        {
            InitializeComponent();
            this.WhenActivated(p => p(ViewModel!.SettingsCommand.Subscribe(Close)));
        }
    }
}
