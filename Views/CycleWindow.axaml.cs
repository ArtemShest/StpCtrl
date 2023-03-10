using Avalonia.Controls;
using Avalonia.ReactiveUI;
using ReactiveUI;
using StpCtrl.ViewModels;
using System;

namespace StpCtrl.Views
{
    public partial class CycleWindow : ReactiveWindow<CycleViewModel>
    {
        public CycleWindow()
        {
            InitializeComponent();
            this.WhenActivated(d => d(ViewModel!.CycleCommand.Subscribe(Close)));
        }
    }
}
