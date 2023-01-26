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
            devices = new();
            devices.Add(new Device("stp_ctrl_1"));
            devices.Add(new Device("stp_ctrl_2"));
            devices.Add(new Device("stp_ctrl_3"));
            devices.Add(new Device("stp_ctrl_4"));
            devices.Add(new Device("stp_ctrl_5"));
            devices.Add(new Device("stp_ctrl_6"));
            devices.Add(new Device("stp_ctrl_7"));
            devices.Add(new Device("stp_ctrl_8"));
            devices.Add(new Device("stp_ctrl_9"));
            devices.Add(new Device("stp_ctrl_10"));
            devices.Add(new Device("stp_ctrl_11"));
            devices.Add(new Device("stp_ctrl_12"));
            devices.Add(new Device("stp_ctrl_13"));
            devices.Add(new Device("stp_ctrl_14"));
            devices.Add(new Device("stp_ctrl_15"));
            devices.Add(new Device("stp_ctrl_16"));
            devices.Add(new Device("stp_ctrl_17"));
            devices.Add(new Device("stp_ctrl_18"));
            devices.Add(new Device("stp_ctrl_19"));
            selectedDevice = devices[0];

            openPanelLength = 150;
//            server.Start(Dns.GetHostEntry(Dns.GetHostName()).AddressList[0], 1234);
            server.Start(1234);

         
        }
    }
}
