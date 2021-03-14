using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace Stanok.ViewModel
{
    public class MainViewModel : ABaseViewModel
    {
        public MainViewModel()
        {
            Knife = new KnifeViewModel();
            Instructions = new InstructionsViewModel();
            Cube = new CubeViewModel();
            Network = new NetworkViewModel();
        }

        public KnifeViewModel Knife { get => Get<KnifeViewModel>(); set => Set(value); }

        public InstructionsViewModel Instructions { get => Get<InstructionsViewModel>(); set => Set(value); }

        public CubeViewModel Cube { get => Get<CubeViewModel>(); set => Set(value); }

        public NetworkViewModel Network { get => Get<NetworkViewModel>(); set => Set(value); }
    }
}
