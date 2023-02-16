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

            openPanelLength = 150;

//            server.Start(1234);

            stepper_tick();

        }

        
    }
}
