using System;

namespace BackupsExtra.Logger
{
    public class ConsoleAlgo : IAlgoLog
    {
        public void Write(string message)
        {
            Console.WriteLine(message);
        }
    }
}