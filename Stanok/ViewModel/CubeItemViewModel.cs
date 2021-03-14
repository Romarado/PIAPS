using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Stanok.ViewModel
{
    /// <summary>
    /// Элемент матрицы бруска
    /// </summary>
    public class CubeItemViewModel: ABaseViewModel
    {
        public CubeItemViewModel(int x, int y, int z)
        {
            X = x; Y = y; Z = z;
        }

        /// <summary>
        /// Позиция X
        /// </summary>
        public int X { get => Get<int>(); set => Set(value); }

        /// <summary>
        /// Позиция Y
        /// </summary>
        public int Y { get => Get<int>(); set => Set(value); }

        /// <summary>
        /// Значение высоты (по оси Z)
        /// </summary>
        public int Z { get => Get<int>(); set => Set(value); }
    }
}
