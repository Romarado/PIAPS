using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using Stanok.Logic;

namespace Stanok
{
    class Program
    {
        public static void Main(string[] args)
        {
            (new Program()).Init();
        }

        private void Init()
        {
            Console.WriteLine("Hello in Stanok console version!\n");
            IManager manager = new Manager(new ConsoleLogger(), Print);
            Configure(manager);
            while (true)
            {
                Console.Write("> ");
                var cmd = Console.ReadLine();
                Console.WriteLine("=======================START=======================");
                switch (cmd)
                {
                    case "start": Run(() => manager.Start(), "start"); break;
                    case "pause": Run(() => manager.Pause(), "pause"); break;
                    case "step": Run(() => manager.MakeStep(), "step"); break;
                    case "stop": Run(() => manager.Stop(), "stop"); break;
                    case "ps": Run(() => Print(manager), "ps"); break;
                    case "conf": Run(() => Configure(manager), "conf"); break;
                    default: Console.WriteLine("Unknown command"); break;
                }
                Console.WriteLine("=======================END=======================");
            }
        }

        private void Run(Action act, string name)
        {
            Console.WriteLine($"Start making \"{name}\"");
            try
            {
                act.Invoke();
            }
            catch (Exception e)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine(e);
                Console.ForegroundColor = ConsoleColor.White;
            }
        }

        private void Configure(IManager manager)
        {
            manager.XMax = 4;
            manager.YMax = 3;
            manager.ZMax = 4;
            manager.Delay = 500;
            manager.Reset();
        }

        private void Print(IManager manager)
        {
            Console.WriteLine("\nCube:");
            var cube = manager.Cube;
            foreach (var row in cube)
            {
                foreach (var cell in row)
                {
                    Console.Write($"{cell}\t");
                }
                Console.WriteLine();
            }
            Console.WriteLine("\n\n");
        }
    }
}
