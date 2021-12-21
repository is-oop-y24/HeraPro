namespace BackupsExtra.Logger
{
    public class Notification
    {
        private readonly IAlgoLog _algoLog;
        public Notification(IAlgoLog algoLog)
        {
            _algoLog = algoLog;
        }

        public void Write(string message) => _algoLog.Write(message);
    }
}