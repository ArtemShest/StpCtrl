using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using ReactiveUI;

namespace StpCtrl.Models
{
    public class Stepper : ReactiveObject
    {
        #region values
        private string? _name;
        private int _curPosition;
        private bool _isPanelOpen;
        private int _target = 0;
        private int _shift = 0;
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
        #endregion

        public Stepper(string name)
        {
            this.name = name;
            this.curPosition = 0;
            this.isPanelOpen = false;
        }


        #region methods
        public void ChangePanelState()
        {
            isPanelOpen = !isPanelOpen;
        }

        public void MoveForward()
        {
        }
        public void MoveBckward()
        {

        }

        #endregion
    }
}
