using Avalonia.Collections;
using ReactiveUI;
using StpCtrl.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive;
using System.Text;
using System.Threading.Tasks;

namespace StpCtrl.ViewModels
{
    public class SettingsViewModel : ViewModelBase
    {
        Stepper? stepper;

        private AvaloniaList<MicrostepMode> _mslist = new();
        public AvaloniaList<MicrostepMode> mslist
        {
            get => _mslist;
            set => this.RaiseAndSetIfChanged(ref _mslist, value);
        }

        private MicrostepMode _ms_mode;
        public MicrostepMode ms_mode
        {
            get => _ms_mode;
            set => this.RaiseAndSetIfChanged(ref _ms_mode, value);
        }


        public SettingsViewModel(Stepper stp)
        {
            mslist = new() { MicrostepMode.whole, MicrostepMode.half, 
                MicrostepMode.quarter, MicrostepMode.eighth, MicrostepMode.sixteenth };

            stepper = new("");
            ms_mode = stp.msMode;
            SettingsCommand = ReactiveCommand.Create(() =>
            {

                if (ms_mode != MicrostepMode.@null) stepper.msMode = ms_mode;
                return stepper;
            });
        }

        public ReactiveCommand<Unit, Stepper?> SettingsCommand { get; }
    }
}
