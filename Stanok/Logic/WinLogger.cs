using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Stanok.Logic
{
    public sealed class WinLogger : Logger
    {
        protected override void Write(string message)
        {
            Console.WriteLine(message);
        }
    }
}
