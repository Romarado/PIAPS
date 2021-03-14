using System;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace Stanok.ViewModel
{
    public class InstructionsViewModel: ABaseViewModel
    {
        /// <summary>
        /// Режим работы станка
        /// </summary>
        public StanokMode Mode { get => Get<StanokMode>(); set => Set(value); }

        /// <summary>
        /// Конечное значение по оси Х
        /// </summary>
        public double MaxX { get => Get<double>(); set => Set(value); }

        /// <summary>
        /// Конечное значение по оси Y
        /// </summary>
        public double MaxY { get => Get<double>(); set => Set(value); }

        /// <summary>
        /// Конечное значение по оси Z
        /// </summary>
        public double MaxZ { get => Get<double>(); set => Set(value); }


        /// <summary>
        /// Задержка шага (мс)
        /// </summary>
        public int Delay { get => Get<int>(); set => Set(value); }

        public InstructionsViewModel()
        {
            MaxX = 5;
            MaxY = 4;
            MaxZ = 3;
            Delay = 500;
        }
    }


    /// <summary>
    /// Режимы работы станка
    /// </summary>
    public enum StanokMode
    {
        /// <summary> Автоматический режим </summary>
        Auto,
        /// <summary> Ручной режим </summary>
        Manual,
        /// <summary> Режим настройки</summary>
        Settings,
    }
}
