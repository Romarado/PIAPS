using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Stanok.Logic
{
    /// <summary>
    /// Интерфейс менеджера логики работы станка
    /// </summary>
    public interface IManager
    {
        #region Public properties

        /// <summary>
        /// Запущен ли станок в автоматическом режиме
        /// </summary>
        bool IsAutoWorking { get; }

        // Настройки работы станка

        /// <summary>
        /// Размер фигуры по оси X
        /// </summary>
        int XMax { get; set; }
        /// <summary>
        /// Размер фигуры по оси Y
        /// </summary>
        int YMax { get; set; }
        /// <summary>
        /// Размер фигуры по оси Z
        /// </summary>
        int ZMax { get; set; }
        /// <summary>
        /// Задержка между шагами в автоматичеком режиме (в мс)
        /// </summary>
        int Delay { get; set; }

        // Текущее положение ножа

        /// <summary>
        /// Текущее положение ножа по оси X
        /// </summary>
        int XKnife { get; }
        /// <summary>
        /// Текущее положение ножа по оси Y
        /// </summary>
        int YKnife { get; }
        /// <summary>
        /// Текущее положение ножа по оси Z
        /// </summary>
        int ZKnife { get; }

        /// <summary>
        /// Текущее состояние бруска (в виде матрицы NxN,
        /// где значение ячейки - количество непустых блоков от 0 по оси Z)
        /// </summary>
        int[][] Cube { get; }

        #endregion // Public properties

        #region Public methods

        /// <summary>
        /// Запуск станка в автоматическом режиме
        /// </summary>
        void Start();
        /// <summary>
        /// Поставить станок
        /// </summary>
        void Pause();
        /// <summary>
        /// Выполнить шаг в ручном режиме (при условии, что станок на паузе / остановлен)
        /// </summary>
        void MakeStep();
        /// <summary>
        /// Выполнить остановку / сброс положения лезвия станка
        /// </summary>
        void Stop();
        /// <summary>
        /// Сбросить все параметры, получить новый брусок
        /// </summary>
        void Reset();

        #endregion // Public methods
    }
}
