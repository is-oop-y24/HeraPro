using System;
using System.Collections.Generic;
using System.Transactions;
using Banks.BankService.ValueObj.Accounts;

namespace Banks.TransactionService
{
    public class Transaction : ITransaction
    {
        private const int DepositOp = 1;
        private const int WithdrawOp = 2;
        private const int TransferOp = 3;
        private int _id;
        private List<double> _contextOfBalance;
        private List<Log> _contextOfLogs;

        public Transaction()
        {
            _contextOfBalance = new List<double>();
            _contextOfLogs = new List<Log>();
            _id = 0;
        }

        public DepositAccount AddDepositAccount(double money, DateTime startPeriod, DateTime endPeriod, double balancePayment)
        {
            using var scope = new TransactionScope();
            _contextOfBalance.Add(money);
            var account = new DepositAccount(_id++, startPeriod, endPeriod, balancePayment);
            scope.Complete();

            return account;
        }

        public DebitAccount AddDebitAccount(double money, double balancePayment)
        {
            using var scope = new TransactionScope();
            _contextOfBalance.Add(money);
            var account = new DebitAccount(_id++, balancePayment);
            scope.Complete();

            return account;
        }

        public CreditAccount AddCreditAccount(double money, int limit, double commission)
        {
            using var scope = new TransactionScope();
            _contextOfBalance.Add(money);
            var account = new CreditAccount(_id++, limit, commission);
            scope.Complete();

            return account;
        }

        public BankAccount AddBankAccount(double money, double commissionForCreditAccount, int limitForCreditAccount, double balancePaymentForDebitAccount, double balancePaymentForDepositAccount)
        {
            using var scope = new TransactionScope();
            _contextOfBalance.Add(money);
            var account = new BankAccount(_id++, commissionForCreditAccount, limitForCreditAccount, balancePaymentForDebitAccount, balancePaymentForDepositAccount);
            scope.Complete();

            return account;
        }

        public void Deposit(Account account, double money)
        {
            using var scope = new TransactionScope();
            _contextOfBalance[account.Id] += money;
            _contextOfLogs.Add(new Log(account.Id, account.Id, money, DepositOp));
            scope.Complete();
        }

        public bool Withdraw(Account account, double money)
        {
            if (_contextOfBalance[account.Id] < money)
                return false;

            using var scope = new TransactionScope();
            _contextOfBalance[account.Id] -= money;
            _contextOfLogs.Add(new Log(account.Id, account.Id, money, WithdrawOp));
            scope.Complete();

            return true;
        }

        public bool Withdraw(DepositAccount account, double money)
        {
            if (account.EndPeriod < DateTime.Now)
                return false;
            if (_contextOfBalance[account.Id] < money)
                return false;

            using var scope = new TransactionScope();
            _contextOfBalance[account.Id] -= money;
            _contextOfLogs.Add(new Log(account.Id, account.Id, money, WithdrawOp));
            scope.Complete();

            return true;
        }

        public bool Withdraw(CreditAccount account, double money)
        {
            double toPay = money;
            if (_contextOfBalance[account.Id] < 0)
                toPay *= account.Commission;
            if (_contextOfBalance[account.Id] < toPay + account.Limit)
                return false;

            using var scope = new TransactionScope();
            _contextOfBalance[account.Id] -= toPay;
            _contextOfLogs.Add(new Log(account.Id, account.Id, toPay, WithdrawOp));
            scope.Complete();

            return true;
        }

        public bool TransferFromTo(Account account1, Account account2, double money)
        {
            if (_contextOfBalance[account1.Id] < money)
                return false;

            using var scope = new TransactionScope();
            _contextOfBalance[account1.Id] -= money;
            _contextOfBalance[account2.Id] += money;
            _contextOfLogs.Add(new Log(account1.Id, account2.Id, money, TransferOp));
            scope.Complete();

            return true;
        }

        public bool TransferFromTo(DepositAccount account1, Account account2, double money)
        {
            if (account1.EndPeriod < DateTime.Now)
                return false;
            if (_contextOfBalance[account1.Id] < money)
                return false;

            using var scope = new TransactionScope();
            _contextOfBalance[account1.Id] -= money;
            _contextOfBalance[account2.Id] += money;
            _contextOfLogs.Add(new Log(account1.Id, account2.Id, money, TransferOp));
            scope.Complete();

            return true;
        }

        public bool TransferFromTo(CreditAccount account1, Account account2, double money)
        {
            double toPay = money;
            if (_contextOfBalance[account1.Id] < 0)
                toPay *= account1.Commission;
            if (_contextOfBalance[account1.Id] < toPay + account1.Limit)
                return false;

            using var scope = new TransactionScope();
            _contextOfBalance[account1.Id] -= money;
            _contextOfBalance[account2.Id] += money;
            _contextOfLogs.Add(new Log(account1.Id, account2.Id, money, TransferOp));
            scope.Complete();

            return true;
        }

        public IEnumerable<Log> GetTransactionsFromIdByOperation(Account account, int operation)
        {
            List<Log> list = _contextOfLogs.FindAll(x => (x.IdFrom == account.Id) && (x.Operation == operation));
            return list.AsReadOnly();
        }

        public bool RollBackTransaction(Log log)
        {
            if (!_contextOfLogs.Contains(log))
                return false;

            switch (log.Operation)
            {
                case 1:
                    _contextOfBalance[log.IdFrom] -= log.Money;
                    break;
                case 2:
                    _contextOfBalance[log.Operation] += log.Money;
                    break;
                case 3:
                {
                    using var scope = new TransactionScope();
                    _contextOfBalance[log.IdTo] -= log.Money;
                    _contextOfBalance[log.IdFrom] += log.Money;
                    scope.Complete();
                    break;
                }
            }

            _contextOfLogs.Remove(log);
            return true;
        }

        public void DoPayment(DebitAccount account)
        {
            _contextOfBalance[account.Id] *= account.BalancePayment;
        }

        public void DoPayment(DepositAccount account)
        {
            _contextOfBalance[account.Id] *= account.BalancePayment;
        }

        public double DoCommission(CreditAccount account, double money)
        {
            return _contextOfBalance[account.Id] > 0 ? money : money * account.Commission;
        }

        public double CheckPayment(DebitAccount account, int days)
        {
            double money = _contextOfBalance[account.Id] * account.BalancePayment * days;
            return money;
        }

        public double CheckPayment(DepositAccount account, int days)
        {
            double money = _contextOfBalance[account.Id] * account.BalancePayment * days;
            return money;
        }
    }
}