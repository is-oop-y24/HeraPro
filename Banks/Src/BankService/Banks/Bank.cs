using System;
using System.Collections.Generic;
using System.Linq;
using System.Transactions;
using Banks.BankEventService;
using Banks.BankLogService;
using Banks.BankService.Accounts;
using Banks.BankService.Accounts.BankAccount;
using Banks.BankService.Accounts.CreditAccount;
using Banks.BankService.Accounts.DebitAccount;
using Banks.BankService.Accounts.DepositAccount;
using Banks.BankService.Clients;
using Banks.BankService.Clients.Builder;
using Banks.Database;

namespace Banks.BankService.Banks
{
    public class Bank : IBank, ISubject
    {
        private readonly IRepository<IClient> _repositoryOfTrustedClients;
        private readonly IRepository<IClient> _repositoryOfUntrustedClients;
        private readonly IRepository<ISubscriber> _repositoryOfSubscribers;
        private readonly ClientBuilder _clientBuilder;

        public Bank(string name, BankAccount account)
        {
            _repositoryOfTrustedClients = new Repository<IClient>();
            _repositoryOfUntrustedClients = new Repository<IClient>();
            _repositoryOfSubscribers = new Repository<ISubscriber>();
            Operations = new List<Log>();
            _clientBuilder = new ClientBuilder();

            Name = name;
            Account = account;
        }

        public List<Log> Operations { get; private set; } // do some rollback logic
        public string Name { get; }
        public BankAccount Account { get; private set; }

        public IClientBuilder CreateClientBuilder()
        {
            return _clientBuilder;
        }

        public IClient AddClient()
        {
            IClient client = _clientBuilder.GetClient();
            if (client == null)
                return null;

            if (client.Passport == null || client.Address == null)
                _repositoryOfUntrustedClients.Add(client);

            _repositoryOfTrustedClients.Add(client);
            return client;
        }

        public DebitAccount AddDebitAccount(Client client, double money)
        {
            var account = new DebitAccount(money, Account.BalancePaymentForDebitAccount);

            client.Accounts.Add(account);
            return account;
        }

        public DepositAccount AddDepositAccount(Client client, double money, DateTime endPeriod)
        {
            var account = new DepositAccount(money, DateTime.Now, endPeriod, Account.PercentByBalanceForDepositAccount);

            client.Accounts.Add(account);
            return account;
        }

        public CreditAccount AddCreditAccount(Client client, double money)
        {
            var account = new CreditAccount(money, Account.LimitForCreditAccount, Account.CommissionForCreditAccount);

            client.Accounts.Add(account);
            return account;
        }

        public void ChangeCommissionForCreditAccount(double commissionForCreditAccount)
        {
            var account = new BankAccount(
                Account.Balance,
                commissionForCreditAccount,
                Account.LimitForCreditAccount,
                Account.BalancePaymentForDebitAccount,
                Account.PercentByBalanceForDepositAccount,
                Account.LimitForUntrustedClients);

            Account = account;
            Notify();
        }

        public void ChangeLimitForCreditAccount(int limitForCreditAccount)
        {
            var account = new BankAccount(
                Account.Balance,
                Account.CommissionForCreditAccount,
                limitForCreditAccount,
                Account.BalancePaymentForDebitAccount,
                Account.PercentByBalanceForDepositAccount,
                Account.LimitForUntrustedClients);

            Account = account;
            Notify();
        }

        public void ChangeBalancePaymentForDebitAccount(double balancePaymentForDebitAccount)
        {
            var account = new BankAccount(
                Account.Balance,
                Account.CommissionForCreditAccount,
                Account.LimitForCreditAccount,
                balancePaymentForDebitAccount,
                Account.PercentByBalanceForDepositAccount,
                Account.LimitForUntrustedClients);

            Account = account;
            Notify();
        }

        public void ChangePercentByBalanceForDepositAccount(List<DepositCommission> percentByBalanceForDepositAccount)
        {
            var account = new BankAccount(
                Account.Balance,
                Account.CommissionForCreditAccount,
                Account.LimitForCreditAccount,
                Account.BalancePaymentForDebitAccount,
                percentByBalanceForDepositAccount,
                Account.LimitForUntrustedClients);

            Account = account;
            Notify();
        }

        public void ChangeLimitForUntrustedClients(int limitForUntrustedClients)
        {
            var account = new BankAccount(
                Account.Balance,
                Account.CommissionForCreditAccount,
                Account.LimitForCreditAccount,
                Account.BalancePaymentForDebitAccount,
                Account.PercentByBalanceForDepositAccount,
                limitForUntrustedClients);

            Account = account;
            Notify();
        }

        public bool TransferFromTo(IClient clientFrom, Account accountFrom, IClient clientTo, Account accountTo, double money)
        {
            if (!clientFrom.Accounts.Contains(accountFrom) || !clientTo.Accounts.Contains(accountTo))
                return false;

            TransactionScope scope;
            if (_repositoryOfTrustedClients.Contains(clientFrom))
            {
                scope = new TransactionScope();
                if (!accountFrom.Withdraw(money)) return false;
                accountTo.Deposit(money);
                Operations.Add(new Log(accountFrom, accountTo, money, OperationEnum.TransferOp));
                scope.Complete();
                return true;
            }

            if (!_repositoryOfUntrustedClients.Contains(clientFrom))
                return false;

            if (Account.LimitForUntrustedClients < money)
                return false;

            scope = new TransactionScope();
            if (!accountFrom.Withdraw(money)) return false;
            accountTo.Deposit(money);
            Operations.Add(new Log(accountFrom, accountTo, money, OperationEnum.TransferOp));
            scope.Complete();
            return true;
        }

        public bool Withdraw(IClient client, Account account, double money)
        {
            if (!client.Accounts.Contains(account)) return false;
            if (_repositoryOfUntrustedClients.Contains(client)) return false;
            if (!_repositoryOfTrustedClients.Contains(client)) return false;

            Operations.Add(new Log(account, account, money, OperationEnum.WithdrawOp));
            return account.Withdraw(money);
        }

        public bool Deposit(IClient client, Account account, double money)
        {
            if (!client.Accounts.Contains(account))
                return false;
            if (!_repositoryOfUntrustedClients.Contains(client) && !_repositoryOfTrustedClients.Contains(client))
                return false;

            Operations.Add(new Log(account, account, money, OperationEnum.DepositOp));
            return account.Deposit(money);
        }

        public void DoCommission()
        {
            var clients = new List<IClient>();
            clients.AddRange(_repositoryOfTrustedClients.GetItemList());
            clients.AddRange(_repositoryOfUntrustedClients.GetItemList());
            foreach (Account account in clients.SelectMany(client => client.Accounts))
            {
                account.DoCommission();
            }
        }

        public void DoPayment()
        {
            var clients = new List<IClient>();
            clients.AddRange(_repositoryOfTrustedClients.GetItemList());
            clients.AddRange(_repositoryOfUntrustedClients.GetItemList());
            foreach (Account account in clients.SelectMany(client => client.Accounts))
            {
                account.DoPayment();
            }
        }

        public IEnumerable<IClient> GetTrustedClientsList()
        {
            return _repositoryOfTrustedClients.GetItemList();
        }

        public IEnumerable<IClient> GetUntrustedClientsList()
        {
            return _repositoryOfUntrustedClients.GetItemList();
        }

        public IEnumerable<IClient> GetClientsList()
        {
            var list = GetTrustedClientsList().ToList();
            list.AddRange(GetUntrustedClientsList());
            return list;
        }

        public double CheckCommission(IClient client, Account account, int days)
        {
            if (!client.Accounts.Contains(account))
                return 0;
            if (!_repositoryOfUntrustedClients.Contains(client) && !_repositoryOfTrustedClients.Contains(client))
                return 0;

            double result = account.CheckCommission(days);
            return result;
        }

        public bool RollBackTransaction(Log log)
        {
            if (!Operations.Contains(log)) return false;
            Operations.Remove(log);

            switch (log.Operation)
            {
                case OperationEnum.TransferOp:
                {
                    var scope = new TransactionScope();
                    log.AccountTo.Balance -= log.Money;
                    log.AccountFrom.Balance += log.Money;
                    scope.Complete();
                    return true;
                }

                case OperationEnum.DepositOp:
                {
                    var scope = new TransactionScope();
                    log.AccountFrom.Balance -= log.Money;
                    scope.Complete();
                    return true;
                }

                case OperationEnum.WithdrawOp:
                {
                    var scope = new TransactionScope();
                    log.AccountFrom.Balance += log.Money;
                    scope.Complete();
                    return true;
                }

                default:
                    return false;
            }
        }

        public void Subscribe(ISubscriber subscriber)
        {
            _repositoryOfSubscribers.Add(subscriber);
            subscriber.Update(Account);
        }

        public void Unsubscribe(ISubscriber subscriber)
        {
            _repositoryOfSubscribers.Remove(subscriber);
        }

        private void Notify()
        {
            foreach (ISubscriber subscriber in _repositoryOfSubscribers.GetItemList())
            {
                subscriber.Update(Account);
            }
        }
    }
}