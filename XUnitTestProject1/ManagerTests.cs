//    using System;
//using System.Threading.Tasks;
//using System.Timers;
//using Xunit;
//using Stanok.Logic;

//namespace XUnitTestProject1
//{
//    public class ManagerTests
//    {
//        public ManagerTests()
//        {
//        }

//        [Fact]
//        public void StartAccept()
//        {
//            _manager.XMax = 2;
//            _manager.YMax = 2;
//            _manager.ZMax = 2;
//            _manager.Delay = TEST_DELAY;
//            _manager.Start();
//            int count = 0;
//            var timer = new Timer();
//            timer.Interval = 4000;
//            timer.Elapsed += (s, e) =>
//            {
//                timer.Stop();
//                if (count == 3)
//                {
//                    _manager.Stop(); 
//                }
//                else if (count % 2 == 0)
//                {
//                    _manager.Pause();
//                    Task.Delay(TEST_DELAY + 1000);
//                    _manager.MakeStep();
//                    _manager.MakeStep();
//                    _manager.MakeStep();
//                }
//                else
//                {
//                    _manager.Start();
//                }
                
//                count++;
//                timer.Start();
//            };
//            timer.Start();
//        }

//        #region Private methods

//        private IManager _manager = new Manager();

//        #endregion Private methods

//        #region Consts

//        private const int TEST_DELAY = 2000;

//        #endregion Consts
//    }
//}
