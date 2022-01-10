using System;

namespace Banks.UI.Console
{
    public class ConsoleUserInterface : IUserInterface
    {
        public string ReadValue(string message)
        {
            System.Console.ForegroundColor = ConsoleColor.Green;
            System.Console.Write(message);
            return System.Console.ReadLine();
        }

        public void WriteMessage(string message)
        {
            System.Console.ForegroundColor = ConsoleColor.Green;
            System.Console.WriteLine(message);
        }

        public void WriteWarning(string message)
        {
            System.Console.ForegroundColor = ConsoleColor.Yellow;
            System.Console.WriteLine(message);
        }
    }
}