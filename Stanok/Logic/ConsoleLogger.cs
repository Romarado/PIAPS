using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Stanok.Logic
{
    sealed class ConsoleLogger : Logger
    {
        protected override void Write(string message)
        {
            Console.WriteLine(message);
        }
    }
}
