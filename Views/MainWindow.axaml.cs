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

namespace StpCtrl.Views
{
    public partial class MainWindow : ReactiveWindow<MainWindowViewModel>
    {
        public MainWindow()
        {
            InitializeComponent();
        }
    }
}
