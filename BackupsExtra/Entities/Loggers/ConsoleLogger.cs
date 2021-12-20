using System;
using BackupsExtra.Services;

namespace BackupsExtra.Entities.Loggers
{
    public class ConsoleLogger : Logger
    {
        public override void MakeLogMessage(string message)
        {
            Console.WriteLine(message);
        }
    }
}