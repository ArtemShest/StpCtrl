using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using System.Net;
using System.Collections;
using Avalonia.Controls;
using System.IO;
using StpCtrl.ViewModels;
using System.Reactive;
using Avalonia.Styling;
using System.Net.NetworkInformation;
using System.Net.Sockets;
using ReactiveUI;
using Avalonia.Collections;
using static System.Net.Mime.MediaTypeNames;

namespace StpCtrl.Models
{
    public class HttpServer : ReactiveObject
    {
        public bool ServerIsStart = false;
        private HttpListener listener;
        public IPAddress? serverIP;
        public int port;
        private string _log = "";
        public bool newClient = false;
        List<string> ipadrr;
        

        public string log
        {
            get => _log; 
            set => this.RaiseAndSetIfChanged(ref _log, value);
        }

        public AvaloniaList<string> clients;

        public HttpServer()
        {
            clients = new();
            listener = new();
            ipadrr = new();
        }

        public void Init(int port)
        {
            listener.Prefixes.Add("http://localhost:" + port.ToString() + "/");
            listener.Prefixes.Add("http://127.0.0.1:" + port.ToString() + "/");
            List<string> listIP = GetAllLocalIPv4(NetworkInterfaceType.Ethernet);
            ipadrr = listIP;
            foreach (string ip in listIP)
                listener.Prefixes.Add("http://" + ip + ":" + port.ToString() + "/");

            listIP = GetAllLocalIPv4(NetworkInterfaceType.Wireless80211);
            foreach (string ip in listIP)
                listener.Prefixes.Add("http://" + ip + ":" + port.ToString() + "/");
        }

        public void Start(int port)
        {
            Init(port);
            Task.Run(() => ServerStart());
        }

        public void Stop()
        {
            if (listener.IsListening)
                listener.Stop();
        }

        async Task ServerStart()
        {
            // установка адресов прослушки
            listener.Start(); // начинаем прослушивать входящие подключения

            while (true)
            {
                // получаем контекст
                var context = await listener.GetContextAsync();
                newClient = true;
                var request = context.Request;  // получаем данные запроса
                var response = context.Response;

                // отправляемый в ответ код htmlвозвращает
                string responseText =
                    @"<!DOCTYPE html>
                    <html>
                        <head>
                            <meta charset='utf8'>
                            <title>METANIT.COM</title>
                        </head>
                        <body>
                            <h2>Hello METANIT.COM</h2>
                        </body>
                    </html>";
                byte[] buffer = Encoding.UTF8.GetBytes(responseText);
                // получаем поток ответа и пишем в него ответ
                response.ContentLength64 = buffer.Length;
                using Stream output = response.OutputStream;
                await output.WriteAsync(buffer);
                await output.FlushAsync();

                
                string[] IP = request.RemoteEndPoint.ToString().Split(new char[] { ':' });
                string clientIP = IP[0];

                bool f = false;
                foreach (string client in clients)
                    if (client == clientIP) f = true;
                if (!f)
                {
                    clients.Add(clientIP);
                    string str = "";
                    foreach (string client in clients) 
                        str += (client + "\n");
                    log = str;
                }
            }
        }

        public static List<string> GetAllLocalIPv4(NetworkInterfaceType _type)
        {
            List<string> ipAddrList = new List<string>();
            foreach (NetworkInterface item in NetworkInterface.GetAllNetworkInterfaces())
            {
                if (item.NetworkInterfaceType == _type && item.OperationalStatus == OperationalStatus.Up)
                {
                    foreach (UnicastIPAddressInformation ip in item.GetIPProperties().UnicastAddresses)
                    {
                        if (ip.Address.AddressFamily == AddressFamily.InterNetwork)
                        {
                            ipAddrList.Add(ip.Address.ToString());
                        }
                    }
                }
            }
            return ipAddrList;
        }
    }
}
