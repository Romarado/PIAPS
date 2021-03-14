using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Media3D;
using Stanok.Logic;

namespace Stanok.ViewModel
{
    public class CubeViewModel: ABaseViewModel
    {
        public CubeViewModel(int sizeX, int sizeY, int sizeZ)
        {
            SizeX = sizeX;
            SizeY = sizeY;
            SizeZ = sizeZ;
            GenetareCube();
        }

        private void GenetareCube()
        {
            Matrix = new CubeItemViewModel[SizeX, SizeY];
            for (int y = 0; y < SizeY; y++)
            {
                for (int x = 0; x < SizeX; x++)
                {
                    Matrix[x, y] = new CubeItemViewModel(x, y, SizeZ);
                }
            }
        }


        /// <summary>
        /// Матрица элементов бруска
        /// </summary>
        public CubeItemViewModel[,] Matrix { get => Get<CubeItemViewModel[,]>(); set => Set(value); }
        
        /// <summary>
        /// Размер бруска по оси X (кол-во элементов)
        /// </summary>
        public int SizeX { get; }

        /// <summary>
        /// Размер бруска по оси У (кол-во элементов)
        /// </summary>
        public int SizeY { get; }

        /// <summary>
        /// Размер бруска по оси Z
        /// </summary>
        public int SizeZ { get; }

    }
}
