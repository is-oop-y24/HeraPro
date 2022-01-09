using Banks.BankService.Accounts;

namespace Banks.BankLogService
{
    public class Log
    {
        internal Log(Account accountFrom, Account accountTo, double money, OperationEnum op)
        {
            AccountFrom = accountFrom;
            AccountTo = accountTo;
            Money = money;
            Operation = op;
        }

        public Account AccountFrom { get; }
        public Account AccountTo { get; }
        public double Money { get; }
        public OperationEnum Operation { get; }
    }
}