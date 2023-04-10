using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using Avalonia;
using Avalonia.Controls;
using StpCtrl.Models;
using System.Net;
using System.Threading.Tasks;
using System.IO;
using System.Threading;
using System.Net.NetworkInformation;
using System.Net.Sockets;
using ReactiveUI;
using System.Reactive;
using System.Windows.Input;
using Avalonia.Collections;
using System.Reactive.Linq;
using System.ComponentModel.Design;

namespace StpCtrl.ViewModels
{
    public class MainWindowViewModel : ViewModelBase
    {
        

        private HttpServer _server = new();
        public HttpServer server
        {
            get => _server;
            set => this.RaiseAndSetIfChanged(ref _server, value);
        }
        public MainWindowViewModel()
        {
            devices = new()
            {
                new Device("stp_ctrl_1")
            };
            selectedDevice = devices[0];

            openPanelLength = 120;

            stepper_tick();

            AvaloniaList<int> CircleCommands = new();
            CycleDialog = new Interaction<CycleViewModel, Stepper>();
            CycleCommand = ReactiveCommand.CreateFromTask(async (Stepper stp) =>
            {
                var store = new CycleViewModel(stp);
                var result = await CycleDialog.Handle(store);
                if (result != null)
                {
                    stp.commands.Clear();
                    for (int i = 0; i < result.commands.Count; i++)
                    {
                        Command comm = new(i + 1);
                        comm.value = result.commands[i].value;
                        stp.commands.Add((comm));
                    }
                    stp.cyclicToolTip = stp.ChangeToolTip(stp.commands);
                    
                }
                return stp;
            });

            SettingsDialog = new Interaction<SettingsViewModel, Stepper>();
            SettingsCommand = ReactiveCommand.CreateFromTask(async (Stepper? stp) =>
            {
                if (stp != null)
                {
                    var store = new SettingsViewModel(stp);
                    var result = await SettingsDialog.Handle(store);
                    if (result != null)
                    {
                        stp.msMode = result.msMode;
                        selectedDevice.changeMS(stp);
                    }
                }
                return stp;
            });
        }

        public ICommand CycleCommand { get; set; }
        public Interaction<CycleViewModel, Stepper> CycleDialog { get; }

        public ICommand SettingsCommand { get; set; }
        public Interaction<SettingsViewModel, Stepper> SettingsDialog { get; }

    }
}
