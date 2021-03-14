using System;
using System.Collections.Generic;

namespace Operator
{
    class Program
    {
        /// <summary>
        /// List of data that console displays
        /// </summary>
        private static LinkedList<string> consoleOutput = new LinkedList<string>();

        static void Main(string[] args)
        {
            // connect to server
            var net = new Network();

            bool serverConnected = true;
            do
            {
                Console.Write("Введите ip: ");
                var ip = Console.ReadLine();
                Console.Write("Введите port: ");
                var port = Convert.ToInt32(Console.ReadLine());
                serverConnected = net.Connect(ip, port);
            } while (!serverConnected);

            // 
            bool runMain = true;
            do
            {
                Console.Clear();
                string choice =
@"Введите команду: 
(1): Переход на автоматический режим
(2): Переход на ручной режим
(3): Переход к настройке
(4): Очередной шаг по программе в ручном режиме и после остановки
(5): Конец работы
(6): Остановка программы

(9): Очистить консоль
(0): Отмена";
                Console.WriteLine(choice);
                Console.WriteLine(string.Join("\n", consoleOutput));
                var command = Console.ReadLine();
                switch (command)
                {
                    case "1":
                        net.SendMessage("0");
                        consoleOutput.AddLast("Переход на автоматический режим");
                        break;
                    case "2":
                        net.SendMessage("1");
                        consoleOutput.AddLast("Переход на ручной режим");
                        break;
                    case "3":
                        var settings = "2";
                        bool runSettings = true;
                        do
                        {
                            string commandChoice =
@"Задать переменную: 
(1): XMAX
(2): YMAX
(3): ZMAX
(4): TMAX

(0): Отмена";
                            Console.WriteLine(commandChoice);
                            var varChoice = Console.ReadLine();
                            switch (varChoice)
                            {
                                case "1":
                                case "2":
                                case "3":
                                case "4":
                                    Console.Write("Введите значение переменной: ");
                                    var variable = Console.ReadLine();
                                    if (int.TryParse(variable, out int n))
                                    {
                                        int.TryParse(varChoice, out int intChoice);
                                        settings += (intChoice - 1) + variable;
                                        net.SendMessage(settings);
                                        consoleOutput.AddLast("Переменная изменена");
                                        runSettings = false;
                                    }
                                    else
                                    {
                                        Console.WriteLine("Введено не число!");
                                    }
                                    break;
                                case "0":
                                    runSettings = false;
                                    break;
                                default:
                                    Console.WriteLine("Неверный ввод");
                                    break;
                            }
                        } while (runSettings);
                        break;
                    case "4":
                        if (net.SendMessage("3"))
                            consoleOutput.AddLast("Шаг по программе в ручном режиме и после остановки");
                        else
                            consoleOutput.AddLast("Ошибка сети");
                        break;
                    case "5":
                        if(net.SendMessage("4"))
                            consoleOutput.AddLast("Конец работы");
                        else
                            consoleOutput.AddLast("Ошибка сети");
                        break;
                    case "6":
                        if(net.SendMessage("5"))
                            consoleOutput.AddLast("Остановка программы");
                        else
                            consoleOutput.AddLast("Ошибка сети");
                        break;
                    case "9":
                        consoleOutput = new LinkedList<string>();
                        break;
                    case "0":
                        runMain = false;
                        break;
                    default:
                        consoleOutput.AddLast("Неверный ввод");
                        break;
                }
            } while (runMain);
        }
    }
}
