using System;
using System.IO;
using SingleResponsibilityPrinciple.Contracts;

namespace SingleResponsibilityPrinciple
{
    public class ConsoleLogger : ILogger
    {
        public void LogWarning(string message, params object[] args)
        {
            Console.WriteLine(string.Concat("WARN: ", message), args);
            WriteToLogFile("WARN", message, args);
        }

        public void LogInfo(string message, params object[] args)
        {
            Console.WriteLine(string.Concat("INFO: ", message), args);
            WriteToLogFile("INFO", message, args);
        }

        // Solves Request 405 - "As a manager, I want to easily monitor the trading errors my traders make so I can provide them better training to avoid errors." 
        private void WriteToLogFile(string messageType, string message, params object[] args)
        {
            using (StreamWriter w = File.AppendText("log.txt")) // in SingleResponsibilityPrincipleTests/bin/Debug
            {
                w.WriteLine("<log><type>" + messageType + "</type><message>" + message + "</message></log>", args);
            }
        }
    }
}
