using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;
using System.Windows.Input;
using System.Windows.Threading;
using Logic = Stanok.Logic;

namespace Stanok.ViewModel
{
    public class MainViewModel : ABaseViewModel
    {
        public MainViewModel()
        {
            Knife = new KnifeViewModel();
            Instructions = new InstructionsViewModel();
            Cube = new CubeViewModel((int)Logic.Manager.X_LENGTH, (int)Logic.Manager.Y_LENGTH, (int)Logic.Manager.Z_LENGTH);
            Network = new NetworkViewModel();
            Log = new Logic.WinLogger();
            Manager = new Logic.Manager(Log, Visit, Instructions);
            // TODO: Заменить
            Manager.Reset();
            Instructions.PropertyChanged += (s, args) =>
            {
                Manager.XMax = (int)Instructions.MaxX;
                Manager.YMax = (int)Instructions.MaxY;
                Manager.ZMax = (int)Instructions.MaxZ;
                Manager.Delay = (int)Instructions.Delay;
                Manager.Reset();
            };
            Instructions.Delay = Instructions.Delay;
        }

        public ICommand AutoCommand { get => Get<ICommand>(); set => Set(value); }

        public KnifeViewModel Knife { get => Get<KnifeViewModel>(); set => Set(value); }
        public InstructionsViewModel Instructions { get => Get<InstructionsViewModel>(); set => Set(value); }
        public CubeViewModel Cube { get => Get<CubeViewModel>(); set => Set(value); }
        public NetworkViewModel Network { get => Get<NetworkViewModel>(); set => Set(value); }

        public Logic.IManager Manager;
        public Logic.ILogger Log;

        private void Visit(Logic.IManager manager)
        {
            Application.Current.Dispatcher.BeginInvoke(new Action(() =>
            {
                Knife.X = manager.XKnife;
                Knife.Y = manager.YKnife;
                Knife.Z = manager.ZKnife;

                for (int y = 0; y < Cube.SizeX; y++)
                {
                    for (int x = 0; x < Cube.SizeY; x++)
                    {
                        Cube.Matrix[x, y].Z = (int) manager.Cube[y][x];
                    }
                }
            }), DispatcherPriority.Background);
        }

        public class RelayCommand : ICommand
        {
            private Action _act;

            public RelayCommand(Action act)
            {
                _act = act;
            }

            public event EventHandler CanExecuteChanged;

            public bool CanExecute(object parameter)
            {
                return true;
            }

            public void Execute(object parameter)
            {
                _act?.Invoke();
            }
        }
    }
}
