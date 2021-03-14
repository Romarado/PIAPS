using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Stanok.ViewModel
{
    public class NetworkViewModel: ABaseViewModel
    {
        /// <summary>
        /// Режим работы
        /// </summary>
        public NetworkMode Mode { get => Get<NetworkMode>(); set => Set(value); }

        /// <summary>
        /// IP Адрес подключения
        /// </summary>
        public string Ip { get => Get<string>(); set => Set(value); }

        /// <summary>
        /// Порт
        /// </summary>
        public int Port { get => Get<int>(); set => Set(value); }

        public NetworkViewModel()
        {
            Ip = "0.0.0.0";
            Port = 1234;
        }
    }

    public enum NetworkMode
    {
        Server,
        Client
    }
}
