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
        private int _currentCoordinate;
        private bool _isPanelOpen;
        #endregion

        #region properties
        public int currentCoordinate
        {
            get => _currentCoordinate;
            set => this.RaiseAndSetIfChanged(ref _currentCoordinate, value);
        }
        public bool isPanelOpen
        {
            get => _isPanelOpen;
            set => this.RaiseAndSetIfChanged(ref _isPanelOpen, value);
        }
        #endregion

        public Stepper()
        {
            this.currentCoordinate = 0;
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
