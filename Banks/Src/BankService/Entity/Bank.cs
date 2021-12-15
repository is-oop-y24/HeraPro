using System;
using Banks.BankService.ValueObj;
using Banks.BankService.ValueObj.Accounts;
using Banks.Database;
using Banks.TransactionService;

namespace Banks.BankService.Entity
{
    public class Bank
    {
        private readonly Repository<Client> _repositoryOfClients;
        private readonly Repository<DebitAccount> _repositoryOfDebitAccounts;
        private readonly Repository<DepositAccount> _repositoryOfDepositAccount;
        private readonly Repository<CreditAccount> _repositoryOfCreditAccount;
        private readonly Transaction _transactionService;
        private DateTime _payment;

        internal Bank(BankAccount bankAccount)
        {
            _repositoryOfClients = new Repository<Client>();
            _repositoryOfDebitAccounts = new Repository<DebitAccount>();
            _repositoryOfDepositAccount = new Repository<DepositAccount>();
            _repositoryOfCreditAccount = new Repository<CreditAccount>();
            _transactionService = new Transaction();
            BankAccount = bankAccount;
            _payment = DateTime.Now;
        }

        public BankAccount BankAccount { get; private set; }

        public Client AddClient(string name)
        {
            var client = new Client(name);
            _repositoryOfClients.Add(client);

            return client;
        }

        public Client AddClient(string name, string address)
        {
            var client = new Client(name, address);
            _repositoryOfClients.Add(client);

            return client;
        }

        public Client AddClient(string name, int passport)
        {
            var client = new Client(name, passport);
            _repositoryOfClients.Add(client);

            return client;
        }

        public Client AddClient(string name, string address, int passport)
        {
            var client = new Client(name, address, passport);
            _repositoryOfClients.Add(client);

            return client;
        }

        public DebitAccount AddDebitAccount(Client client, double money)
        {
            DebitAccount account = _transactionService.AddDebitAccount(money, BankAccount.BalancePaymentForDebitAccount);
            var clientToReturn = new Client(client, account.Id);
            _repositoryOfClients.Update(client, clientToReturn);
            _repositoryOfDebitAccounts.Add(account);

            return account;
        }

        public DepositAccount AddDepositAccount(Client client, double money, DateTime startPeriod, DateTime endPeriod)
        {
            DepositAccount account = _transactionService.AddDepositAccount(money, startPeriod, endPeriod, BankAccount.BalancePaymentForDepositAccount);
            var clientToReturn = new Client(client, account.Id);
            _repositoryOfClients.Update(client, clientToReturn);
            _repositoryOfDepositAccount.Add(account);

            return account;
        }

        public CreditAccount AddCreditAccount(Client client, double money)
        {
            CreditAccount account = _transactionService.AddCreditAccount(money, BankAccount.LimitForCreditAccount, BankAccount.CommissionForCreditAccount);
            var clientToReturn = new Client(client, account.Id);
            _repositoryOfClients.Update(client, clientToReturn);
            _repositoryOfCreditAccount.Add(account);

            return account;
        }

        public void ChangeCommissionForCreditAccounts(double commission)
        {
            var bankAccount = new BankAccount(BankAccount.Id, commission, BankAccount.LimitForCreditAccount, BankAccount.BalancePaymentForDebitAccount, BankAccount.BalancePaymentForDepositAccount);
            BankAccount = bankAccount;
        }

        public void ChangeLimitForCreditAccounts(int limit)
        {
            var bankAccount = new BankAccount(BankAccount.Id, BankAccount.CommissionForCreditAccount, limit, BankAccount.BalancePaymentForDebitAccount, BankAccount.BalancePaymentForDepositAccount);
            BankAccount = bankAccount;
        }

        public void ChangeBalancePaymentForDebitAccount(double balancePayment)
        {
            var bankAccount = new BankAccount(BankAccount.Id, BankAccount.CommissionForCreditAccount, BankAccount.LimitForCreditAccount, balancePayment, BankAccount.BalancePaymentForDepositAccount);
            BankAccount = bankAccount;
        }

        public void ChangeBalancePaymentFOrDepositAccount(double balancePayment)
        {
            var bankAccount = new BankAccount(BankAccount.Id, BankAccount.CommissionForCreditAccount, BankAccount.LimitForCreditAccount, BankAccount.BalancePaymentForDebitAccount, balancePayment);
            BankAccount = bankAccount;
        }

        public void CheckPayment()
        {
            TimeSpan days = DateTime.Now - _payment;
            for (int i = 0; i < days.Days; ++i)
            {
                DoPayment();
            }

            _payment = DateTime.Now;
        }

        private void DoPayment()
        {
            foreach (DebitAccount i in _repositoryOfDebitAccounts.GetItemList())
            {
                _transactionService.DoPayment(i);
            }

            foreach (DepositAccount i in _repositoryOfDepositAccount.GetItemList())
            {
                _transactionService.DoPayment(i);
            }
        }
    }
}