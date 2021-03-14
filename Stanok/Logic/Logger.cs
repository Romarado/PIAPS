using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Stanok.Logic
{
    public abstract class Logger : ILogger
    {
        public void Info(string message)
        {
            Write($"INFO: {message}");
        }

        public void Warn(string message)
        {
            Write($"WARN: {message}");
        }

        public void Error(string message)
        {
            Write($"ERROR: {message}");
        }

        protected abstract void Write(string message);
    }
}
