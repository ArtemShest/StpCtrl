using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using Avalonia.Collections;
using ReactiveUI;

namespace StpCtrl.Models
{
    public class Stepper : ReactiveObject
    {
        #region values
        private string? _name;
        private int _curPosition;
        private Int64 _curPositionMM;
        private bool _isPanelOpen;
        private int _target = 0;
        private int _shift = 0;
        private bool _isShowSteps = true;
        private bool _isShowCurrentMM = false;
        private string _unitMeasureShiftOn = "steps";
        private string _unitMeasureMoveTo = "steps";
        private AvaloniaList<Command>? _commands;
        private string _cyclicToolTip;
        #endregion

        #region properties
        public string? name
        {
            get => _name;
            set => this.RaiseAndSetIfChanged(ref _name, value);
        }
        public int curPosition
        {
            get => _curPosition;
            set => this.RaiseAndSetIfChanged(ref _curPosition, value);
        }
        public Int64 curPositionMM
        {
            get => _curPositionMM;
            set => this.RaiseAndSetIfChanged(ref _curPositionMM, value);
        }
        public bool isPanelOpen
        {
            get => _isPanelOpen;
            set => this.RaiseAndSetIfChanged(ref _isPanelOpen, value);
        }
        public int target
        {
            get => _target;
            set
            {
                if (value > 9999999) value = 9999999;
                if (value < -9999999) value = -9999999;
                this.RaiseAndSetIfChanged(ref _target, value);
            }
        }
        public int shift
        {
            get => _shift;
            set
            {
                if (value > 9999999) value = 9999999;
                if (value < -9999999) value = -9999999;
                this.RaiseAndSetIfChanged(ref _shift, value);
            }
        }

        public bool isShowSteps
        {
            get => _isShowSteps;
            set
            {
                if (!value) { isShowCurrentMM = true; }
                this.RaiseAndSetIfChanged(ref _isShowSteps, value);
            }
        }
        public bool isShowCurrentMM
        {
            get => _isShowCurrentMM;
            set
            {
                if (!value) { isShowSteps = true; }
                this.RaiseAndSetIfChanged(ref _isShowCurrentMM, value);
            }
        }
        public string unitMeasureShiftOn
        {
            get => _unitMeasureShiftOn;
            set => this.RaiseAndSetIfChanged(ref _unitMeasureShiftOn, value);
        }
        public string unitMeasureMoveTo
        {
            get => _unitMeasureMoveTo;
            set => this.RaiseAndSetIfChanged(ref _unitMeasureMoveTo, value);
        }
        public AvaloniaList<Command>? commands
        {
            get => _commands;
            set
            {
                this.RaiseAndSetIfChanged(ref _commands, value);
                
            }
        }

        public string cyclicToolTip
        {
            get => _cyclicToolTip;
            set => this.RaiseAndSetIfChanged(ref _cyclicToolTip, value);
        }



        #endregion

        public Stepper(string name)
        {
            this.name = name;
            this.curPosition = 0;
            this.isPanelOpen = false;
            this.commands = new();
        }


        #region methods
        public void ChangePanelState()
        {
            isPanelOpen = !isPanelOpen;
        }

        public void ChangeUnitMeasureShiftOn()
        {
            if (unitMeasureShiftOn == "steps") unitMeasureShiftOn = "mkm";
            else if (unitMeasureShiftOn == "mkm") unitMeasureShiftOn = "steps";
        }

        public void ChangeUnitMeasureMoveTo()
        {
            if (unitMeasureMoveTo == "steps") unitMeasureMoveTo = "mkm";
            else if (unitMeasureMoveTo == "mkm") unitMeasureMoveTo = "steps";
        }

        public string ChangeToolTip(AvaloniaList<Command> cmds)
        {
            if (cmds == null || cmds.Count == 0)
                return "Set commands";
            else
            {
                string str = "";
                for(int i = 0; i< cmds.Count; i++) 
                {
                    str += cmds[i].index + ". " + cmds[i].value + "\n";
                }
                return str;
            }
        }

        #endregion
    }

    public class Command : ReactiveObject
    {
            private int _index;
        public int index
        {
            get => _index;
            set => this.RaiseAndSetIfChanged(ref _index, value);
        }
            private int _value;
        public int value
        {
            get => _value;
            set => this.RaiseAndSetIfChanged(ref _value, value);
        }
        public Command(int index)
        {
            this.index = index;
            value = 0;
        }
    }
}
