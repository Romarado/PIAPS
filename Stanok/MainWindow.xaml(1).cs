using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

using Stanok.Logic;
using Stanok.Server;
using Stanok.ViewModel;

namespace Stanok
{
    public partial class MainWindow : Window
    {
        MainViewModel viewModel = new MainViewModel();

        public MainWindow()
        {
            InitializeComponent();

            DataContext = viewModel;

            viewModel.Knife.Z = 6;
            
        }

        CancellationTokenSource demoToken = new CancellationTokenSource();

        private void buttonStart_Click(object sender, RoutedEventArgs e)
        {
            Run(viewModel.Manager.Start);
        }

        private void buttonPause_Click(object sender, RoutedEventArgs e)
        {
            Run(viewModel.Manager.Pause);
        }

        private void buttonNextStep_Click(object sender, RoutedEventArgs e)
        {
            Run(viewModel.Manager.MakeStep);
        }

        private void buttonStop_Click(object sender, RoutedEventArgs e)
        {
            Run(viewModel.Manager.Stop);
        }

        private void ButtonReset_Click(object sender, RoutedEventArgs e)
        {
            viewModel.Manager.Reset();
        }

        private void ButtonConnect_Click(object sender, RoutedEventArgs e)
        {
            StanokServer server;
            Task.Factory.StartNew(() =>
            {
                var ip = viewModel.Network.Ip;
                var port = viewModel.Network.Port;
                switch (viewModel.Network.Mode)
                {
                    case NetworkMode.Server:
                        server = new StanokServer(port, ip, viewModel.Manager);
                        break;
                    case NetworkMode.Client:
                        break;
                }
            });
        }


        private void Run(Action act)
        {
            try
            {
                act.Invoke();
            }
            catch (Exception e)
            {
                viewModel.Log.Error(e.Message);
                MessageBox.Show(e.Message);
            }
        }

    }
}
