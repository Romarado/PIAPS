using System;
using System.Collections.Generic;
using System.Linq;
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
            // Эмуляция работы
            // TODO: Заменить на настоящую логику
            demoToken = new CancellationTokenSource();
            new Task(() =>
            {
                viewModel.Knife.X = 0;
                viewModel.Knife.Y = 0;
                viewModel.Knife.Z = viewModel.Cube.Matrix[0, 0].Z;

                for (int j = 0; j < viewModel.Cube.SizeY; j++)
                {
                    for (int i = 0; i < viewModel.Cube.SizeX; i++)
                    {
                        if (demoToken.IsCancellationRequested)
                            return;

                        viewModel.Knife.Z = viewModel.Cube.Matrix[i, j].Z;
                        viewModel.Cube.Matrix[i, j].Z--;

                        viewModel.Knife.X += 1;
                        if (viewModel.Knife.X >= viewModel.Cube.SizeX)
                            viewModel.Knife.X = 0;

                        Thread.Sleep(100);
                    }
                    viewModel.Knife.Y += 1;
                }
            }).Start();
        }

        private void buttonPause_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Не реализованно");
        }

        private void buttonNextStep_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Не реализованно");
        }

        private void buttonStop_Click(object sender, RoutedEventArgs e)
        {
            //MessageBox.Show("Не реализованно");

            demoToken.Cancel();
        }

        private void ButtonConnect_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Не реализованно");
        }
    }
}
