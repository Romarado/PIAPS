using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Media3D;

namespace Stanok.ViewModel
{
    public class CubeViewModel: ABaseViewModel
    {
        public CubeViewModel()
        {
            GenetareCube();
        }

        private void GenetareCube()
        {
            // Создаём матрицу
            var cube = new int[SizeX, SizeY];

            // Заполняем матрицу
            //var random = new Random();
            for (int x = 0; x < SizeX; x++)
                for (int y = 0; y < SizeY; y++)
                    cube[x, y] = //random.Next(0, sizeZ);
                        (7 <= y && y <= 18 && x < 20) ? 4 : 7;

            // Копируем значения из матрицы
            Matrix = new CubeItemViewModel[SizeX, SizeY];
            for (int x = 0; x < SizeX; x++)
                for (int y = 0; y < SizeY; y++)
                    Matrix[x,y] = new CubeItemViewModel(x, y, cube[x, y]);
        }


        /// <summary>
        /// Матрица элементов бруска
        /// </summary>
        public CubeItemViewModel[,] Matrix { get => Get<CubeItemViewModel[,]>(); set => Set(value); }
        
        /// <summary>
        /// Размер бруска по оси X (кол-во элементов)
        /// </summary>
        public int SizeX { get; private set; } = 25;

        /// <summary>
        /// Размер бруска по оси У (кол-во элементов)
        /// </summary>
        public int SizeY { get; private set; } = 25;

        /// <summary>
        /// Размер бруска по оси Z
        /// </summary>
        public int SizeZ { get; private set; } = 8;

    }
}
