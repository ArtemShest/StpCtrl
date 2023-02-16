using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Sockets;
using System.Reactive;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Avalonia.Collections;
using ReactiveUI;

namespace StpCtrl.Models
{
    public class Device : ReactiveObject
    {
        #region values
        private string? _ip;
        private string? _mac;
        private string? _name;
        private byte? _command;
        private byte? _cmdAxis;
        private List<byte>? _cmdData;

        private AvaloniaList<Stepper> _axis;
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

        public byte? command
        {
            get => _command;
            set => this.RaiseAndSetIfChanged(ref _command, value);
        }
        public byte? cmdAxis
        {
            get => _cmdAxis;
            set => this.RaiseAndSetIfChanged(ref _cmdAxis, value);
        }
        public List<byte>? cmdData
        {
            get => _cmdData;
            set => this.RaiseAndSetIfChanged(ref _cmdData, value);
        }
        public AvaloniaList<Stepper> axis
        {
            get => _axis;
            set => this.RaiseAndSetIfChanged(ref _axis, value);
        }



        #endregion

        public Device(string name)
        {
            this.name = name;
            cmdData = new();
            axis = new()
            {
                new Stepper("axis A"),
                new Stepper("axis B")
            };

            moveForward = ReactiveCommand.Create<Stepper>(_moveForward);
            stop = ReactiveCommand.Create<Stepper>(_stop);
            moveBackward = ReactiveCommand.Create<Stepper>(_moveBackward);
            setPositionZero = ReactiveCommand.Create<Stepper>(_setPositionZero);
            moveToZero = ReactiveCommand.Create<Stepper>(_moveToZero);
            moveTo = ReactiveCommand.Create<Stepper>(_moveTo);
            shiftOn = ReactiveCommand.Create<Stepper>(_shiftOn);
        }

        #region commands
        public ReactiveCommand<Stepper, Unit> moveForward { get; }
        public ReactiveCommand<Stepper, Unit> stop { get; }
        public ReactiveCommand<Stepper, Unit> moveBackward { get; }
        public ReactiveCommand<Stepper, Unit> setPositionZero { get; }
        public ReactiveCommand<Stepper, Unit> moveToZero { get; }
        public ReactiveCommand<Stepper, Unit> moveTo { get; }
        public ReactiveCommand<Stepper, Unit> shiftOn { get; }
        #endregion

        #region methods
        public void _moveForward(Stepper step)
        {
            cmdAxis = (byte)axis.IndexOf(step);
            command = 2;
        }
        public void _stop(Stepper step)
        {
            cmdAxis = (byte)axis.IndexOf(step);
            command = 1; 
        }
        public void _moveBackward(Stepper step)
        {
            cmdAxis = (byte)axis.IndexOf(step);
            command = 3;
        }
        public void _setPositionZero(Stepper step)
        {
            cmdAxis = (byte)axis.IndexOf(step);
            command = 5;
        }
        public void _moveToZero(Stepper step)
        {
            cmdAxis = (byte)axis.IndexOf(step);
            command = 6; 
        }
        public void _moveTo(Stepper step)
        {

            cmdData = intToListByte(step.target);
            cmdAxis = (byte)axis.IndexOf(step);
            command = 7;
        }
        public void _shiftOn(Stepper step)
        {

            cmdData = intToListByte(step.shift);
            cmdAxis = (byte)axis.IndexOf(step);
            command = 8; 
        }

        public List<byte> intToListByte(int data)
        {
            List<byte> list = new List<byte>();
            for (int i = 3; i >= 0; i--)
            {
                list.Add((byte)(data >> (8 * i)));
            }
            return list;
        }

        public void check_curPosition()
        {
            sendCommand(9, 1, null);
        }

        async public void sendCommand(byte? __cmd, byte? __axis, List<byte>? data)
        {
            while (__cmd == null) ;
            if (true)
            {
                byte _axis = (byte)__axis; byte _cmd = (byte)__cmd;
                TcpClient tcpClient = new TcpClient();
                try
                {
                    //Thread.Sleep(1000);
                    byte[] requestData = { 0, 0, 0, 0, _cmd, _axis, 0, 0, 0, 0 };
                    if (data != null)
                        for (int i = 0; i < 4; i++)
                        {
                            requestData[i + 6] = data[i];
                        }

                    List<byte> msg = new();
//                    await tcpClient.ConnectAsync("192.168.1.15", 10);
                    tcpClient.Connect("192.168.1.15", 10);
                    NetworkStream stream = tcpClient.GetStream();
                    await stream.WriteAsync(requestData);
                    int count = 0;
                    while (!stream.DataAvailable)
                    {
                        if (count >= 50) break;
                        count++;
                        Thread.Sleep(10);
                    }
                    while (stream.DataAvailable)
                    {
                        msg.Add(Convert.ToByte(stream.ReadByte()));
                    }

                    /*
                    do 
                    { 
                        msg.Add(Convert.ToByte(stream.ReadByte())); 
                    }
                    while (stream.DataAvailable);
                    */
                    int curPositionA = msg[0];
                    for (int i = 1; i < 4; i++) { curPositionA = (curPositionA << 8) + msg[i]; }
                    int curPositionB = msg[4];
                    axis[0].curPosition = curPositionA;
                    
                    for (int i = 4; i < 8; i++) { curPositionB = (curPositionB << 8) + msg[i]; }
                    axis[1].curPosition = curPositionB;
                    tcpClient.Close();
                }//
                catch 
                {
                    if (tcpClient.Connected) tcpClient.Close();
                }
            }
            
        }
        #endregion


    }
}
