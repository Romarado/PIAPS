using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Stanok.ViewModel
{
    public class KnifeViewModel: ABaseViewModel
    {
        public KnifeViewModel()
        {
            MinX = MinY = MinZ = 0;
            MaxX = MaxY = 24;
            MaxZ = 6;
        }

        /// <summary>
        /// Текущее положение ножа по оси X
        /// </summary>
        public double X { get => Get<double>(); set => Set(value); }

        /// <summary>
        /// Текущее положение ножа по оси Y
        /// </summary>
        public double Y { get => Get<double>(); set => Set(value); }

        /// <summary>
        /// Текущее положение ножа по оси Z
        /// </summary>
        public double Z { get => Get<double>(); set => Set(value); }


        /// <summary>
        /// Минимальное положение ножа по оси X
        /// </summary>
        public double MinX { get => Get<double>(); set => Set(value); }

        /// <summary>
        /// Минимальное положение ножа по оси Y
        /// </summary>
        public double MinY { get => Get<double>(); set => Set(value); }

        /// <summary>
        /// Минимальное положение ножа по оси Z
        /// </summary>
        public double MinZ { get => Get<double>(); set => Set(value); }


        /// <summary>
        /// Максимальное положение ножа по оси X
        /// </summary>
        public double MaxX { get => Get<double>(); set => Set(value); }

        /// <summary>
        /// Максимальное положение ножа по оси Y
        /// </summary>
        public double MaxY { get => Get<double>(); set => Set(value); }

        /// <summary>
        /// Максимальное положение ножа по оси Z
        /// </summary>
        public double MaxZ { get => Get<double>(); set => Set(value); }
    }
}
