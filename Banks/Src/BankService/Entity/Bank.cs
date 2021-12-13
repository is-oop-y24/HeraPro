using System.Collections.Generic;
using Banks.BankService.ValueObj;
using Banks.BankService.ValueObj.Accounts;
using Banks.Database;
using Banks.TransactionService;

namespace Banks.BankService.Entity
{
    public class Bank
    {
        private Repository<Client> _repositoryOfClients;
        private Repository<Account> _repositoryOfAccounts;
        private Transaction _transactionService;

        internal Bank(BankAccount bankAccount)
        {
            _repositoryOfClients = new Repository<Client>();
            _repositoryOfAccounts = new Repository<Account>();
            _transactionService = new Transaction();
            BankAccount = bankAccount;
        }

        public BankAccount BankAccount { get; }

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

        public DebitAccount AddDebitAccount(Client client, int money)
        {
            DebitAccount account = _transactionService.AddDebitAccount(money);
            var clientToReturn = new Client(client, account.Id);
            _repositoryOfClients.Update(client, clientToReturn);

            return account;
        }

        public DepositAccount AddDepositAccount(Client client, int money)
        {
            DepositAccount account = _transactionService.AddDepositAccount(money);
            var clientToReturn = new Client(client, account.Id);
            _repositoryOfClients.Update(client, clientToReturn);

            return account;
        }

        public CreditAccount AddCreditAccount(Client client, int money)
        {
            CreditAccount account = _transactionService.AddCreditAccount(money);
            var clientToReturn = new Client(client, account.Id);
            _repositoryOfClients.Update(client, clientToReturn);

            return account;
        }
        
        public void 
        
    }
}