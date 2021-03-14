using Stanok.Logic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace Stanok.Server
{
    class StanokServer
    {
        #region Variables
        private static int _port;
        private static bool _isWork = false;
        private static string _ipAddress;

        public int Port
        {
            get;
            set;
        }
        public string IpAddress
        {
            get;
            set;
        }
        #endregion

        #region Constructor
        public StanokServer(int port, string ip, IManager manager)
        {
            IPAddress localAddr = IPAddress.Parse(ip);
            _isWork = true;

            var server = new TcpListener(localAddr, port);

            server.Start();
            
            while(_isWork)
            {
                TcpClient client = server.AcceptTcpClient();
                NetworkStream stream = client.GetStream();

                while(client.Connected)
                {
                    byte[] msg = new byte[256];
                    int count = stream.Read(msg, 0, msg.Length);
                    var message = Encoding.UTF8.GetString(msg, 0, count);
                    var command = message;
                    string[] messages = new string[2];
                    int variable = 0;
                    int variableName=-1;
                    if (message.Length > 1)
                    {
                        command = message.Length != 1 ? message.Remove(1, count - 1) : message;
                        messages[0] = message[1].ToString();
                        messages[1] = message.Substring(2);
                        if (messages.Length == 2)
                        {
                            Int32.TryParse(messages[0], out variableName);
                            Int32.TryParse(messages[1], out variable);
                        }
                    }
                    switch (command)
                    {
                        case "0":
                            manager.Start();
                            break;
                        case "1":
                            manager.Pause();
                            break;
                        case "2":
                            switch (variableName)
                            {
                                case 0:
                                    manager.XMax = variable;
                                    (manager as Manager).UpdateInstructions("XMax");
                                    break;
                                case 1:
                                    manager.YMax = variable;
                                    (manager as Manager).UpdateInstructions("YMax");
                                    break;
                                case 2:
                                    manager.ZMax = variable;
                                    (manager as Manager).UpdateInstructions("ZMax");
                                    break;
                                case 3:
                                    manager.Delay = variable;
                                    (manager as Manager).UpdateInstructions("Delay");
                                    break;
                            }
                            break;
                        case "3":
                            manager.MakeStep();
                            break;
                        case "4":
                            manager.Stop();
                            break;
                        case "5":
                            manager.Reset();
                            break;
                    }
                }
            }              
        }

        public void Close()
        {
            _isWork = false;
        }
        #endregion
    }
}
