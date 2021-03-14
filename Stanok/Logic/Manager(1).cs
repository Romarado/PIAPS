using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Timers;
using Stanok.ViewModel;
using Timer = System.Timers.Timer;

namespace Stanok.Logic
{
    public class Manager : IManager
    {
        #region .Ctor

        public Manager(ILogger log, Action<IManager> visit = null, InstructionsViewModel instructionsVM = null)
        {
            _instructionsVM = instructionsVM;
            _Log = log;
            Init(visit);
        }

        #endregion

        #region Public properties

        /// <summary>
        /// Запущен ли станок в автоматическом режиме
        /// </summary>
        public bool IsAutoWorking => _isWorking;

        // Настройки работы станка

        /// <summary>
        /// Размер фигуры по оси X
        /// </summary>
        public int XMax { get; set; }
        /// <summary>
        /// Размер фигуры по оси Y
        /// </summary>
        public int YMax { get; set; }
        /// <summary>
        /// Размер фигуры по оси Z
        /// </summary>
        public int ZMax { get; set; }
        /// <summary>
        /// Задержка между шагами в автоматичеком режиме (в мс)
        /// </summary>
        public int Delay { get; set; }

        // Текущее положение ножа

        /// <summary>
        /// Текущее положение ножа по оси X
        /// </summary>
        public int XKnife { get; private set; }
        /// <summary>
        /// Текущее положение ножа по оси Y
        /// </summary>
        public int YKnife { get; private set; }
        /// <summary>
        /// Текущее положение ножа по оси Z
        /// </summary>
        public int ZKnife { get; private set; }

        /// <summary>
        /// Текущее состояние бруска (в виде матрицы NxN,
        /// где значение ячейки - количество непустых блоков от 0 по оси Z)
        /// </summary>
        public int[][] Cube => _cube;

        #endregion Public properties

        #region Public methods

        /// <summary>
        /// Запуск станка в автоматическом режиме
        /// </summary>
        public void Start()
        {
            if (_isWorking && !_isPaused)
                throw new Exception("Станок уже запущен");
            if (_isPaused)
            {
                _isPaused = false;
                _Log.Info("Станок возобновил работу");
                return;
            }
            _isWorking = true;
            _Log.Info("Станок начал работу");
            Task.Factory.StartNew(() =>
            {
                while (_isWorking)
                {
                    if (!_isPaused || _hasHandleRequest)
                    {
                        foreach (var step in InternalSteps())
                        {
                            _hasHandleRequest = false;
                            step();
                            if (!_isWorking)
                                return;
                            if (_isPaused)
                                break;
                        }
                    }

                    Task.Delay(TimeSpan.FromMilliseconds(Delay)).Wait();
                }

                var message = "Станок завершил работу";
                _Log.Info(message);
            });
        }
        /// <summary>
        /// Поставить выполнение на паузу
        /// </summary>
        public void Pause()
        {
            if (!_isWorking)
                throw new Exception("Станок не запущен");
            if (_isPaused)
                throw new Exception("Станок уже на паузе");
            _isPaused = true;
            _Log.Info("Станок поставлен на паузу");
        }
        /// <summary>
        /// Выполнить шаг в ручном режиме (при условии, что станок на паузе / остановлен)
        /// </summary>
        public void MakeStep()
        {
            if (!_isPaused)
                throw new Exception("Станок не поставлен на паузу");
            _hasHandleRequest = true;
            _Log.Info("Запрос на выполнение шага");
        }
        /// <summary>
        /// Выполнить остановку / сброс положения лезвия станка
        /// </summary>
        public void Stop()
        {
            if (!_isWorking)
                throw new Exception("Станок не запущен");
            _tokenSource.Cancel();
            ResetKnifePosition();
            _Log.Info("Станок остановлен");
        }
        /// <summary>
        /// Сбросить все параметры, получить новый брусок
        /// </summary>
        public void Reset()
        {
            Init();
            _Log.Info("Настройки станка сброшены");
        }

        public void UpdateInstructions(string name)
        {
            switch (name)
            {
                case "XMax":
                    _instructionsVM.MaxX = XMax;
                    break;
                case "YMax":
                    _instructionsVM.MaxY = YMax;
                    break;
                case "ZMax":
                    _instructionsVM.MaxZ = ZMax;
                    break;
                case "Delay":
                    _instructionsVM.Delay = Delay;
                    break;
            }
        }

        #endregion Public methods

        #region Private fields

        private ILogger _Log;

        // Наш куб для работы
        private int[][] _cube;
        // Токен отмены для приостановки работы
        private CancellationTokenSource _tokenSource;
        // Находится ли станок в режиме паузы
        private bool _isPaused;
        private bool _isWorking;
        private bool _hasHandleRequest;
        private readonly InstructionsViewModel _instructionsVM;
        private Action<IManager> _visit;

        #endregion Private fields

        #region Consts

        public const int X_LENGTH = 20;
        public const int Y_LENGTH = 20;
        public const int Z_LENGTH = 4;

        #endregion Consts

        #region Private methods

        // Шаги
        private IEnumerable<Action> InternalSteps()
        {
            if (ZKnife == Z_LENGTH + 1)
            {
                yield return () => InternalMakeStep(XKnife, YKnife, ZKnife - 1);
            }

            var z0 = ZKnife;
            var isBegin = true;
            for (int z = z0; z > Z_LENGTH - ZMax; z--)
            {
                var y0 = YKnife;
                if (z % 2 == 0)
                {
                    for (int y = y0; y <= YMax; y++)
                    {
                        int x0 = XKnife;
                        if (y % 2 == 0)
                        {
                            x0 += (int) (isBegin ? 1 : 0);
                            isBegin = false;
                            for (int x = x0; x <= XMax; x++)
                            {
                                yield return () => InternalMakeStep(x, y, z);
                            }
                        }
                        else
                        {
                            x0 -= (int) (isBegin ? 1 : 0);
                            isBegin = false;
                            for (int x = x0; x >= 0; x--)
                            {
                                yield return () => InternalMakeStep(x, y, z);
                            }
                        }
                    }
                }
                else
                {
                    for (int y = y0; y >= 0; y--)
                    {
                        int x0 = XKnife;
                        if (y % 2 != 0)
                        {
                            x0 += (int)(isBegin ? 1 : 0);
                            isBegin = false;
                            for (int x = x0; x <= XMax; x++)
                            {
                                yield return () => InternalMakeStep(x, y, z);
                            }
                        }
                        else
                        {
                            x0 -= (int)(isBegin ? 1 : 0);
                            isBegin = false;
                            for (int x = x0; x >= 0; x--)
                            {
                                yield return () => InternalMakeStep(x, y, z);
                            }
                        }
                    }
                }
            }
            ResetKnifePosition();
        }

        private void InternalMakeStep(int x, int y, int z)
        {
            XKnife = x;
            YKnife = y;
            ZKnife = z;
            _cube[y][x] = z - 1;
            _visit?.Invoke(this);
            _Log.Info("Станок выполнил шаг");
            Task.Delay(TimeSpan.FromMilliseconds(Delay)).Wait();
        }

        // Сброс положения ножа
        private void ResetKnifePosition()
        {
            _isPaused = false;
            _isWorking = false;
            XKnife = 0;
            YKnife = 0;
            ZKnife = Z_LENGTH + 1;
        }

        // Сбросить все значения на значения по умолчанию
        private void Init(Action<IManager> visit = null)
        {
            if (visit != null)
                _visit = visit;
            ResetKnifePosition();
            _tokenSource = new CancellationTokenSource();
            _hasHandleRequest = false;
            _cube = new int[X_LENGTH][];
            for (int i = 0; i < X_LENGTH; i++)
            {
                _cube[i] = new int[Y_LENGTH];
                for (int j = 0; j < Y_LENGTH; j++)
                {
                    _cube[i][j] = Z_LENGTH;
                }
            }
            _visit(this);
        }

        #endregion Private methods
    }
}
