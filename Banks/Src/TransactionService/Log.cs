namespace Banks.TransactionService
{
    public class Log
    {
        internal Log(int idFrom, int idTo, double money, int operation)
        {
            IdFrom = idFrom;
            IdTo = idTo;
            Money = money;
            Operation = operation;
        }

        public int IdFrom { get; }
        public int IdTo { get; }
        public double Money { get; }
        public int Operation { get; }
    }
}