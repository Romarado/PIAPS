using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Stanok.Logic
{
    class Manager : IManager
    {
        #region Public properties

        /// <summary>
        /// Запущен ли станок в автоматическом режиме
        /// </summary>
        public bool IsAutoWorking { get; private set; }

        // Настройки работы станка

        /// <summary>
        /// Размер фигуры по оси X
        /// </summary>
        public uint XMax { get; set; }
        /// <summary>
        /// Размер фигуры по оси Y
        /// </summary>
        public uint YMax { get; set; }
        /// <summary>
        /// Размер фигуры по оси Z
        /// </summary>
        public uint ZMax { get; set; }
        /// <summary>
        /// Задержка между шагами в автоматичеком режиме
        /// </summary>
        public uint Delay { get; set; }

        // Текущее положение ножа

        /// <summary>
        /// Текущее положение ножа по оси X
        /// </summary>
        public uint XKnife { get; }
        /// <summary>
        /// Текущее положение ножа по оси Y
        /// </summary>
        public uint YKnife { get; }
        /// <summary>
        /// Текущее положение ножа по оси Z
        /// </summary>
        public uint ZKnife { get; }

        /// <summary>
        /// Текущее состояние бруска (в виде матрицы NxN,
        /// где значение ячейки - количество непустых блоков от 0 по оси Z)
        /// </summary>
        public uint[,] Cube { get; }

        #endregion // Public properties

        #region Public methods

        /// <summary>
        /// Запуск станка в автоматическом режиме
        /// </summary>
        public void Start()
        {
            uint x0 = XKnife;
            uint y0 = YKnife;
            uint z0 = ZKnife;
            for (uint z = z0; z <= ZMax; z++)
            {
                for (uint y = y0; y <= YMax; y++)
                {
                    for (uint x = x0; x <= XMax; x++)
                    {
                        internalMakeStep();
                    }   
                }
            }
        }
        /// <summary>
        /// Поставить станок
        /// </summary>
        public void Pause()
        {

        }
        /// <summary>
        /// Выполнить шаг в ручном режиме (при условии, что станок на паузе / остановлен)
        /// </summary>
        public void MakeStep()
        {

        }
        /// <summary>
        /// Выполнить остановку / сброс положения лезвия станка
        /// </summary>
        public void Stop()
        {

        }
        /// <summary>
        /// Сбросить все параметры, получить новый брусок
        /// </summary>
        public void Reset()
        {

        }

        #endregion // Public methods

        #region Private methods

        private void internalMakeStep()
        {
            
        }

        #endregion Private methods
    }
}
