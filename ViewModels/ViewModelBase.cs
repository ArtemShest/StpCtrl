using Avalonia.Collections;
using ReactiveUI;
using StpCtrl.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace StpCtrl.ViewModels
{
    public class ViewModelBase : ReactiveObject
    {
        #region values

        private AvaloniaList<Device>? _devices; // devices
        private Device? _selectedDevice;        // selectedDevice
        private bool _isPanelOpen;              // isPanelOpen
        private int _openPanelLength;           // openPanelLength
        private bool _sliderState;              // sliderState

        private string _log = "";
        #endregion


        #region properties

        public AvaloniaList<Device> devices
        {
            get => _devices;
            set => this.RaiseAndSetIfChanged(ref _devices, value);
        }

        public Device selectedDevice
        {
            get => _selectedDevice;
            set => this.RaiseAndSetIfChanged(ref _selectedDevice, value);
        }
        public bool isPanelOpen
        {
            get => _isPanelOpen;
            set => this.RaiseAndSetIfChanged(ref _isPanelOpen, value);
        }
       
        public int openPanelLength
        {
            get => _openPanelLength;
            set => this.RaiseAndSetIfChanged(ref _openPanelLength, value);
        }

        public bool sliderState
        {
            get => _sliderState;
            set => this.RaiseAndSetIfChanged(ref _sliderState, value);
        }

        public string log
        {
            get => _log;
            set => this.RaiseAndSetIfChanged(ref _log, value);
        }

        #endregion

        #region methods
        public void ChangePanelState()
        {
            isPanelOpen = !isPanelOpen;
        }

        public void ChangeSliderState()
        {
            sliderState = !sliderState;
        }

        public void Log(string str)
        {
            log += str+"\n";
        }

        
        #endregion



    }
}
