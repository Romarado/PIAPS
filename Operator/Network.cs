using System;
using System.Net;
using System.Net.Sockets;
using System.Text;

namespace Operator
{
    class Network
    {
        /// <summary>
        /// server port
        /// </summary>
        private int port;

        /// <summary>
        /// server ip
        /// </summary>
        private string ip;

        private TcpClient tcpClient;

        /// <summary>
        /// tcp client network stream 
        /// </summary>
        private NetworkStream stream;

        public Network() { }

        public void SetPort(int port)
        {
            this.port = port;
        }

        public void SetIp(string ip)
        {
            this.ip = ip;
        }

        public bool Connect()
        {
            try
            {
                var ipAddress = IPAddress.Parse(ip);
                tcpClient = new TcpClient();
                tcpClient.Connect(ipAddress, port);
                stream = tcpClient.GetStream();
                Console.WriteLine("Успешное подключение!");
                return true;
            }
            catch (SocketException e)
            {
                Console.WriteLine("SocketException: {0}", e);
            }
            catch (Exception e)
            {
                Console.WriteLine("Exception: {0}", e.Message);
            }
            return false;
        }

        public bool Connect(string ip, int port)
        {
            SetPort(port);
            SetIp(ip);
            return Connect();
        }

        public bool CloseConnection()
        {
            try
            {
                stream.Close();
                tcpClient.Close();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public bool SendMessage(string message)
        {
            try
            {
                byte[] sendData = Encoding.UTF8.GetBytes(message);
                stream.Write(sendData, 0, sendData.Length);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}
