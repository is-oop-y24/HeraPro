using System.Collections.Generic;
using System.Transactions;
using Banks.BankService.ValueObj;
using Banks.BankService.ValueObj.Accounts;

namespace Banks.TransactionService
{
    public class Transaction : ITransaction
    {
        private const int DepositOp = 1;
        private const int WithdrawOp = 2;
        private const int TransferOp = 3;
        private int _id;
        private List<int> _contextOfAccounts;
        private List<Log> _contextOfLogs;

        public Transaction()
        {
            _contextOfAccounts = new List<int>();
            _contextOfLogs = new List<Log>();
            _id = 0;
        }

        public DepositAccount AddDepositAccount(int money)
        {
            using var scope = new TransactionScope();
            _contextOfAccounts.Add(money);
            var account = new DepositAccount(_id++);
            scope.Complete();

            return account;
        }

        public DebitAccount AddDebitAccount(int money)
        {
            using var scope = new TransactionScope();
            _contextOfAccounts.Add(money);
            var account = new DebitAccount(_id++);
            scope.Complete();

            return account;
        }

        public CreditAccount AddCreditAccount(int money)
        {
            using var scope = new TransactionScope();
            _contextOfAccounts.Add(money);
            var account = new CreditAccount(_id++);
            scope.Complete();

            return account;
        }

        public BankAccount AddBankAccount(int money)
        {
            using var scope = new TransactionScope();
            _contextOfAccounts.Add(money);
            var account = new BankAccount(_id++);
            scope.Complete();

            return account;
        }

        public void Deposit(int id, int money)
        {
            using var scope = new TransactionScope();
            _contextOfAccounts[id] += money;
            _contextOfLogs.Add(new Log(id, id, money, DepositOp));
            scope.Complete();
        }

        public bool Withdraw(int id, int money)
        {
            if (_contextOfAccounts[id] < money)
                return false;

            using var scope = new TransactionScope();
            _contextOfAccounts[id] -= money;
            _contextOfLogs.Add(new Log(id, id, money, WithdrawOp));
            scope.Complete();

            return true;
        }

        public bool TransferFromTo(int id1, int id2, int money)
        {
            if (_contextOfAccounts[id1] < money)
                return false;

            using var scope = new TransactionScope();
            _contextOfAccounts[id1] -= money;
            _contextOfAccounts[id2] += money;
            _contextOfLogs.Add(new Log(id1, id2, money, TransferOp));
            scope.Complete();

            return true;
        }

        public IEnumerable<Log> GetTransactionsFromIdByOperation(int id, int operation)
        {
            List<Log> list = _contextOfLogs.FindAll(x => (x.IdFrom == id) && (x.Operation == operation));
            return list.AsReadOnly();
        }

        public bool RollBackTransaction(Log log)
        {
            if (!_contextOfLogs.Contains(log))
                return false;

            switch (log.Operation)
            {
                case 1:
                    _contextOfAccounts[log.IdFrom] -= log.Money;
                    break;
                case 2:
                    _contextOfAccounts[log.Operation] += log.Money;
                    break;
                case 3:
                {
                    using var scope = new TransactionScope();
                    _contextOfAccounts[log.IdTo] -= log.Money;
                    _contextOfAccounts[log.IdFrom] += log.Money;
                    scope.Complete();
                    break;
                }
            }

            _contextOfLogs.Remove(log);
            return true;
        }
    }
}