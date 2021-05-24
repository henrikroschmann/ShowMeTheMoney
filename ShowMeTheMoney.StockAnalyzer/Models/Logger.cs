using System;
using System.Diagnostics;

namespace ShowMeTheMoney.StockAnalyzer.Models
{
    public static class Logger
    {
        public static Action<string> WriteEvent { get; set; }

        /// <summary>
        /// Write formatted debug output.
        /// </summary>
        /// <param name="_message"></param>
        public static void Write(string _message)
        {
            string message = _message;

            if (WriteEvent == null)
                Debug.Write(message);
            else
                WriteEvent(message);
        }

        /// <summary>
        /// Write formatted debug output, and start new line.
        /// </summary>
        /// <param name="_message"></param>
        public static void WriteLine(string _message)
        {
            Write($"{DateTime.Now} {_message}");
            Write(Environment.NewLine);
        }
    }
}
