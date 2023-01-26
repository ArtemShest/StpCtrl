using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using ReactiveUI;

namespace StpCtrl.Models
{
    public class Device : ReactiveObject
    {
        #region values
        private int? _id;
        private string? _ip;
        private string? _mac;
        private string? _name;
        private Stepper _axisA;
        private Stepper _axisB;
        #endregion

        #region properties
        public string ip
        {
            get => _ip;
            set => this.RaiseAndSetIfChanged(ref _ip, value);
        }

        public string name
        {
            get => _name;
            set => this.RaiseAndSetIfChanged(ref _name, value); 
        }

        public Stepper axisA
        {
            get => _axisA;
            set => this.RaiseAndSetIfChanged(ref _axisA, value);
        }
        public Stepper axisB
        {
            get => _axisB;
            set => this.RaiseAndSetIfChanged(ref _axisB, value);
        }

        #endregion

        public Device(string name)
        {
            this.name = name;
            axisA = new Stepper();
            axisB = new Stepper();

        }

        #region methods
        public void axisA_MoveForward()
        {
            sendCommand(2, 1);
        }
        public void axisA_MoveBackward()
        {
            sendCommand(3, 1);
        }
        public void axisA_Stop()
        {
            sendCommand(1, 1);
        }


        public void axisB_MoveForward()
        {
            sendCommand(2, 2);
        }
        public void axisB_MoveBackward()
        {
            sendCommand(3, 2);
        }
        public void axisB_Stop()
        {
            sendCommand(1, 2);
        }

        public void sendCommand(byte cmd, byte axis)
        {
            try
            {
                TcpClient tcpClient = new TcpClient();
                tcpClient.Connect("192.168.1.7", 10);
                NetworkStream stream = tcpClient.GetStream();
                //byte[] byte_ip = new byte[4];
                //string[] str_ip = ip.Split(new char[] { '.' });
                //for (int i = 0; i < 4; i++) byte_ip[i] = Convert.ToByte(str_ip[i]);

                byte[] requestData = { 0, 0, 0, 0, cmd, axis, 0, 0, 0, 0 };
                stream.Write(requestData);
                tcpClient.Close();
            }//
            catch { }
        }
        #endregion


    }
}
