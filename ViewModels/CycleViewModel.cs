using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ReactiveUI;
using System.Threading.Tasks;
using System.Reactive;
using Avalonia.Collections;
using StpCtrl.Models;
using Avalonia.Input;
using System.Security.Cryptography;

namespace StpCtrl.ViewModels
{
    public class CycleViewModel : ViewModelBase
    {
        private Stepper? _stepper;
        public Stepper? stepper
        {
            get => _stepper;
            set => this.RaiseAndSetIfChanged(ref _stepper, value);
        }

        private Command? _selectedCommand;
        public Command? selectedCommand
        {
            get => _selectedCommand;
            set => this.RaiseAndSetIfChanged(ref _selectedCommand, value);
        }

        public CycleViewModel(Stepper stp)
        {

            stepper = new("");

            for(int i = 0; i < stp.commands.Count; i++)
            {
                Command comm = new(i+1);
                comm.value = stp.commands[i].value;
                stepper.commands.Add((comm));
            }

            CycleCommand = ReactiveCommand.Create(() =>
            {

                return stepper;
            });
        }

        public ReactiveCommand<Unit, Stepper> CycleCommand { get; }

        public void AddCommand()
        {
            if (stepper != null && stepper.commands == null) 
                stepper.commands = new();
            stepper?.commands?.Add(new Command(stepper.commands.Count+1));
        }

        public void DeleteCommand()
        {    
            Command cmd = selectedCommand;
            stepper?.commands?.Remove(cmd);
            for(int i = 0; i < stepper?.commands?.Count; i++)
            {
                stepper.commands[i].index = i + 1;
            }
            selectedCommand = null;
        }
    }

}
